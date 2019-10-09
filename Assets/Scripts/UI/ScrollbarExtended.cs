using UnityEngine;
using UnityEngine.UI;

public class ScrollbarExtended : Scrollbar {

	public Button up;
	public Button down;

	public new void Start () {
		up = transform.Find("Up")?.GetComponent<Button>();
		down = transform.Find("Down")?.GetComponent<Button>();

		if (up != null) up.onClick.AddListener(()=>Scroll(-1));
        if (down != null) down.onClick.AddListener(()=>Scroll(1));
	}

	private void Scroll(int i){
		if(i < 0){
			this.value += this.size / 10f;
		}else{
			this.value -= this.size / 10f;
		}
	}
}
