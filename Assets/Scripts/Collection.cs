using System.Collections.Generic;

[System.Serializable]
public class Collection {

    public static Collection current = null;

    private string Title;
    private string Description;
    
    private NestedList<string> Groups;
    private List<Entry> Entries;
    

}
