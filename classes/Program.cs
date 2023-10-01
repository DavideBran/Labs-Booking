
class Program{
    
    static void Main(){
        Student st= new("Davide", "Brancato"); 
        Computer[] pc= new Computer[4];
        pc[0] = new Computer(1, "Photoshop VisualStudio Notion UbuntWSL".Split(" "));
        pc[1] = new Computer(2, "Photoshop VisualStudio Notion UbuntuWSL".Split(" "));
        pc[2] = new Computer(3, "Photoshop VisualStudio Notion UbuntuWSL".Split(" "));
        pc[3] = new Computer(4, "Photoshop VisualStudio Notion UbuntuWSL".Split(" "));

        Labs newLab= new(0, pc);

        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 11:30:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("02/10/2023 2:00:00 PM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, "Photoshop", DateTime.Parse("03/10/2023 9:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, null, DateTime.Parse("03/10/2023 11:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, null, DateTime.Parse("03/10/2023 10:00:00 AM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, null, DateTime.Parse("03/10/2023 1:00:00 PM", System.Globalization.CultureInfo.InvariantCulture), newLab);
        st.BookWs(2, null, DateTime.Parse("03/10/2023 4:00:00 PM", System.Globalization.CultureInfo.InvariantCulture), newLab);




    }
}