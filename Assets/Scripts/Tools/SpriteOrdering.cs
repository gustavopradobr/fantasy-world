using UnityEngine;

public class SpriteOrdering : MonoBehaviour {

    [SerializeField] private int sortingOrderOrigin = 0;
    private SpriteRenderer[] spriteRenderers;
    [SerializeField] private bool updateOrdering = false;

    private void Start () {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        SortingOrder();
    }

    private void Update()
    {
        if (updateOrdering)
        {
            SortingOrder();
        }
    }

    private void SortingOrder()
    {
        int sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100);

        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sortingOrder = sortingOrderOrigin - sortingOrder;
    }

    public void ResetOrdering()
    {
        int min = -32768;
        for (int i = 0; i < spriteRenderers.Length; i++)
            spriteRenderers[i].sortingOrder = min;
    }
}
