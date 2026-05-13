namespace CirkusLuna.Model
{
    //Artist class, arver fra Person
    public class Artist : Person
    {
        //Specifikke properties til Artist
        public string Act {  get; set; } = string.Empty; //Det de kan, f.eks junglere, akrobat m.m
    
        //Constructor
        public Artist(int id, string firstName, string lastName, string email, string act)
            : base(id, firstName, lastName, email) //Delte fields håndteres af base class
        {
            Act = act;
        }
    }

}
