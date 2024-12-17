using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class PokemonSlot : MonoBehaviour
{
    public Sprite pokemonSprite; // Hình ảnh của Pokémon
    public GameObject pokemonObject; // Prefab của Pokémon
    public int price; // Giá của Pokémon
    public Image icon; // Hình ảnh hiển thị
    public TextMeshProUGUI priceText; // Văn bản hiển thị giá
    public TextMeshProUGUI cooldownText; // Văn bản hiển thị thời gian cooldown

    private GameManager gms; 
    private Dp dpManager; // Tham chiếu đến lớp quản lý DP
    private float cooldownTime = 5f; // Thời gian cooldown (giây)
    private float nextBuyTime = 0f; // Thời gian tiếp theo có thể mua

    private void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        dpManager = GameObject.Find("DpManager").GetComponent<Dp>(); // Tìm đối tượng quản lý DP
        GetComponent<Button>().onClick.AddListener(BuyPokemon); 
        UpdateSlotUI(); 

        cooldownText.enabled = false; // Ẩn văn bản cooldown ban đầu
    }

   private void UpdateSlotUI()
   {
       if (pokemonSprite)
       {
           icon.enabled = true;
           icon.sprite = pokemonSprite;
           priceText.text = price.ToString() + "DP";
       }
       else
       {
           icon.enabled = false; 
       }
   }

   private void BuyPokemon()
   {
       if (Time.time < nextBuyTime)
       {
           return; // Nếu đang trong cooldown, không thực hiện mua
       }

       if (dpManager != null && dpManager.CurrentDp >= price) // Kiểm tra xem có đủ DP để mua không
       {
           gms.BuyPokemon(pokemonObject, pokemonSprite); 
           dpManager.CurrentDp -= price; // Trừ giá trị DP khi mua Pokémon
           nextBuyTime = Time.time + cooldownTime; // Cập nhật thời gian tiếp theo có thể mua
           StartCoroutine(CooldownEffect()); // Bắt đầu hiệu ứng cooldown
       }
       else
       {
           Debug.Log("Không đủ DP để mua Pokémon!"); // Ghi log nếu không đủ DP
       }
   }

   private IEnumerator CooldownEffect()
   {
       cooldownText.enabled = true; // Hiện văn bản countdown

       float remainingTime = cooldownTime;

       while (remainingTime > 0)
       {
           cooldownText.text = Mathf.Ceil(remainingTime).ToString(); // Cập nhật thời gian còn lại
           remainingTime -= Time.deltaTime; // Giảm thời gian còn lại
           yield return null; // Đợi đến frame tiếp theo
       }

       cooldownText.enabled = false; // Ẩn văn bản countdown khi kết thúc
   }

   private void OnValidate()
   {
       UpdateSlotUI(); 
   }
}
