namespace Ocean.XamarinForms.Controls {
    using Xamarin.Forms;

    public class FastImageCell : ImageCell {

        public FastImageCell() {
        }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            var bc = this.BindingContext as IFastImageCellData;
            if (bc != null) {
                this.Text = bc.Text;
                this.Detail = bc.Detail;
                this.ImageSource = bc.ImageSource;
            }
        }

    }
}
