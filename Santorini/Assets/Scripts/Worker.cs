using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each Player has a male and female worker that they can move around the board and build around
/// </summary>
public class Worker : MonoBehaviour
{
    [SerializeField]
    GameObject _highlight = default;

    God _god = default;
    Tile _tile = default;

    public enum Gender
    {
        Female,
        Male
    }

    public enum Colour
    {
        Blue,
        White
    }

    Gender _gender = Gender.Female;
    Colour _colour = Colour.Blue;

    public void Initialize(Gender gender, Colour colour)
    {
        _gender = gender;
        _colour = colour;
    }

    public void EnableHighlight()
    {
        _highlight.SetActive(true);
    }

    public void DisableHighlight()
    {
        _highlight.SetActive(false);
    }

    public void SetGod(God god)
    {
        _god = god; 
    }

    public God GetGod()
    {
        return _god;
    }

    public void SetTile(Tile tile)
    {
        _tile = tile;
    }

    public Tile GetTile()
    {
        return _tile;
    }
    
    public Gender GetGender()
    {
        return _gender;
    }

    public Colour GetColour()
    {
        return _colour;
    }
}
