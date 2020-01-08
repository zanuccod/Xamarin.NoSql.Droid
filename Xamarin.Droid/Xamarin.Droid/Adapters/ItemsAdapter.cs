using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Common.ViewModels;
using LiteDb.Common.Entities;

namespace Xamarin.Droid.Adapters
{
    public class ItemsViewHolder : RecyclerView.ViewHolder
    {
        public TextView Model { get; private set; }
        public TextView Productor { get; private set; }

        public ItemsViewHolder(View itemView, Action<int> onClickListener, Action<int> onLongClickListener)
            : base(itemView)
        {
            // Locate and cache view references:
            Model = itemView.FindViewById<TextView>(Resource.Id.adapter_item_model);
            Productor = itemView.FindViewById<TextView>(Resource.Id.adapter_item_productor);

            itemView.Click += (sender, e) => onClickListener(AdapterPosition);
            itemView.LongClick += (sender, e) => onLongClickListener(AdapterPosition);
        }
    }

    public class ItemsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<Car> ItemClick;
        public event EventHandler<Car> ItemLongClick;

        private readonly Activity activity;
        private readonly ItemsViewPresenter<Car> viewModel;


        public ItemsAdapter(Activity activity, ItemsViewPresenter<Car> viewModel)
        {
            this.activity = activity;
            this.viewModel = viewModel;

            // update UI if collection changes
            this.viewModel.Items.CollectionChanged += (sender, args) =>
            {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        private void OnClick(int position) => ItemClick?.Invoke(this, viewModel.Items[position]);
        private void OnLongClick(int position) => ItemLongClick?.Invoke(this, viewModel.Items[position]);

        public override int ItemCount => viewModel.Items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ItemsViewHolder vh = holder as ItemsViewHolder;

            vh.Model.Text = viewModel.Items[position].Model;
            vh.Productor.Text = vh.Model.Text = string.Format("{0} {1}", viewModel.Items[position].Productor, viewModel.Items[position].Year.ToString());
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                                          Inflate(Resource.Layout.adapter_item, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            return new ItemsViewHolder(itemView, OnClick, OnLongClick);
        }
    }
}
