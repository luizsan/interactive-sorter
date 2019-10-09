using UnityEngine;
using System.Collections.Generic;

///
public class Sorter : MonoBehaviour {

    public enum Choice {
        Left,
        Right,
        Tie,
    }


    public TextAsset asset;

    private int progress = 0;
    private int total = 0;
    private int size = 0;

    private NestedList<Entry> current = new NestedList<Entry>();
    private NestedList<Entry> next = new NestedList<Entry>();
    private List<Entry> left = new List<Entry>();
    private List<Entry> right = new List<Entry>();
    private List<Entry> sorted = new List<Entry>();


    public void Start() {
        if (!asset) {
            Debug.LogWarning("File not found.");
            return;
        }

        string[] lines = asset.text.Trim().Split('\n');
        Entry[] source = new Entry[lines.Length];

        if (lines.Length < 2) {
            Debug.LogWarning("Cannot sort. Text file has only one entry.");
            return;
        } else {
            for (int i = 0; i < lines.Length; i++) {
                lines[i] = lines[i].Trim();
                source[i] = new Entry(){ Name = lines[i] };
            }

            size = source.Length;
            total = LoopCount(size);
            Debug.Log(total);
            current.Initialize(source);
            Fetch();
        }
    }

    public void Reset() {
        progress = 0;
        total = 0;
        size = 0;
        current = new NestedList<Entry>();
        next = new NestedList<Entry>();
        left = new List<Entry>();
        right = new List<Entry>();
        sorted = new List<Entry>();
    }

    public int LoopCount(int n) {
        if (n < 2) return 0;

        int count = 0;
        List<int> current = new List<int>(n);
        List<int> next = new List<int>(n);

        for (int i = 0; i < n; i++){
            current.Add(1);
        }

        while (true) {
            if (current.Count > 1) {
                int sum = current[0] + current[1];
                count += sum - 1;
                next.Add(sum);
                current.RemoveRange(0, 2);
            } else if (current.Count > 0) {
                int sum = current[0];
                next.Add(sum);
                current.RemoveAt(0);
            } else {
                if (next[0] == n) {
                    return count;
                }
                current = new List<int>(next);
                next.Clear();
            }
        }
    }

    public void Fetch(){
        if(left.Count > 0 || right.Count > 0){
            Debug.LogWarning($"{left[0].Name} vs {right[0].Name}");
            return;
        }

        if (current.Count > 1) {
            left.AddRange(new List<Entry>(current[0]));
            right.AddRange(new List<Entry>(current[1]));
            Debug.LogWarning($"{left[0].Name} vs {right[0].Name}");
            current.RemoveRange(0, 2);
            return;
        } else if (current.Count > 0) {
            next.Add(new List<Entry>(current[0]));
            current.RemoveAt(0);
        } 

        if(!Cycle()){
            Debug.Log($"End: {progress} // Total: {total}");
            return;
        }

        Fetch();
        
    }

    public bool Cycle() {
        current.Clear();
        foreach (List<Entry> item in next) {
            current.Add(new List<Entry>(item));
            if(item.Count == size){
                next.Clear();
                return false;
            }
        }

        next.Clear();
        return true;
    }

    public void Pick(Choice c) {
        switch (c) {
            case Choice.Left:
                progress += 1;
                sorted.Add(left[0]);
                left.RemoveAt(0);
                break;

            case Choice.Right:
                progress += 1;
                sorted.Add(right[0]);
                right.RemoveAt(0);
                break;

            // case Choice.Tie: 
            //     if(left.Count == 1 || right.Count == 1){
            //         progress += 1;
            //     }else{
            //         progress += 2;
            //     }
            //     sorted.Add(left[0]);
            //     sorted.Add(right[1]);
            //     left.RemoveAt(0);
            //     right.RemoveAt(0);
            //     break;

            default:
                break;
        }

        if (left.Count == 0 || right.Count == 0) {
            progress += left.Count + right.Count - 1;
            Flush();
        }
        
        Debug.Log(progress);
        Fetch();
    }

    public void Flush() {
        sorted.AddRange(new List<Entry>(left));
        sorted.AddRange(new List<Entry>(right));
        next.Add(new List<Entry>(sorted));
        
        left.Clear();
        right.Clear();
        sorted.Clear();
    }

    public void Update(){
        if(progress >= total) return;
        Pick((Choice)Random.Range(0,2));
    }
}
