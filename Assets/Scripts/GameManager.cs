using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentPokemon; // Prefab của Pokémon
    public Sprite currentPokomonSprite; // Sprite của Pokémon
    public Transform tiles; // Các tile trong game
    public LayerMask tileMask; // Layer mask cho raycast
    public void BuyPokemon(GameObject pokemon, Sprite sprite)
    {
        currentPokemon = pokemon;
        currentPokomonSprite = sprite;
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);
        foreach(Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(hit.collider && currentPokemon)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPokomonSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(currentPokemon, hit.collider.transform.position, Quaternion.identity);
                currentPokemon = null;
                currentPokomonSprite = null;
            }
        }
    }
}