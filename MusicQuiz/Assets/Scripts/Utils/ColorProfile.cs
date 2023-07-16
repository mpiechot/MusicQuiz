using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ColorProfile", order = 1)]
public class ColorProfile : ScriptableObject
{
    public Color backgroundColor;
    public Color primaryColor;
    public Color secondaryColor;
    public Color defaultColor;

    public Color gamesColor;
    public Color animeColor;
    public Color serienColor;
    public Color movieColor;
    public Color disneyColor;

    public Color solvedColor;
    public Color unknownColor;
    public Color closeToSolvedColor;
    public Color wrongColor;
    


    public Color GetCategoryColor(string category) => category switch
    {
        "games" => gamesColor,
        "anime" => animeColor,
        "serien" => serienColor,
        "movies" => movieColor,
        "disney" => disneyColor,
        _ => defaultColor,
    };
}