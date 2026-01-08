using UnityEngine;

public class LayoutSkinCards : MonoBehaviour
{
    [SerializeField]
    private SkinScriptableObject[] skinCardDatas;
    [SerializeField]
    private GameObject prefab;


    [ContextMenu("Generate Cards")]
    private void GenerateCardsMenu()
    {
        GenerateCards();
    }

    public void GenerateCards()
    {
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        int cardIndex = 0;
        foreach (var cardData in skinCardDatas)
        {
            GameObject cardInstance = Instantiate(prefab, transform);
            cardInstance.name = $"Card_{cardIndex}_{cardData.skinName}";

            SkinCard skinCard = cardInstance.GetComponent<SkinCard>();
            skinCard.skinScriptable = cardData;

            cardIndex++;
        }    
    }
}
