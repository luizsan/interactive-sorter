using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutOverride : MonoBehaviour {

	public Vector2 MinimumCellSize = new Vector2(200f,220f);

	[System.NonSerialized] private GridLayoutGroup grid;
	[System.NonSerialized] private RectTransform self;

	[System.NonSerialized] private float width = 0f;
	[System.NonSerialized] private int columns = 1;
	[System.NonSerialized] private Vector2 size = new Vector2();

	private void Update(){
		grid = GetComponent<GridLayoutGroup>();
		self = GetComponent<RectTransform>();

		if(grid == null || self == null) return;
		width = self.rect.width - grid.padding.left - grid.padding.right + grid.spacing.x;
		columns = (int)(width / MinimumCellSize.x);
		size.x = ( width / (float)columns ) - grid.spacing.x;
		size.y = MinimumCellSize.y;
		grid.cellSize = size;
	}

}
