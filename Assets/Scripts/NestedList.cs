using System.Collections.Generic;

public class NestedList<T> : List<List<T>> {

    public void Initialize(params T[] items) {
        foreach (T item in items) {
            this.Add(new List<T>() { item });
        }
    }

    public void Initialize(params List<T>[] items) {
        foreach (List<T> list in items) {
            this.Add(new List<T>(list));
        }
    }
    
}