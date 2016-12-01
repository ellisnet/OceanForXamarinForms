namespace Ocean.XamarinFormsSamples.Model {
    using System;
    using Ocean.XamarinForms.Controls;

    public class Speaker : IFastImageCellData {

        public String Avatar { get; set; }

        public String Description { get; set; }

        String IFastImageCellData.Detail => this.Title;

        public String Id { get; set; }

        String IFastImageCellData.ImageSource => this.Avatar;

        public String Name { get; set; }

        String IFastImageCellData.Text => this.Name;

        public String Title { get; set; }

        public String Website { get; set; }

        public Speaker() {
        }

    }
}
