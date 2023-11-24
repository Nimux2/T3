using System;
using Godot;

namespace T3Projet.Tools.Gestion;

public class ImagesPatient
{
    public ImagesPatient(string imageDefault, string imageTriste, string imageContent , string imagePeur , string imageColere)
    {
        this.imageDefault = imageDefault;
        this.imageTriste = imageTriste;
        this.imageContent = imageContent;
        this.imagePeur = imagePeur;
        this.imageColere = imageColere;
    }

    private string imageDefault = null;
    private string imageTriste = null;
    private string imageContent = null;
    private string imagePeur = null;
    private string imageColere = null;

    private Types actuelType = Types.DEFAULT;
    public Types ActualType
    {
        get => actuelType;
    }
    
    public string GetImageForEnum(Types type)
    {
        actuelType = type;
        if (type == Types.DEFAULT)
        {
            return imageDefault;
        }
        else if (type == Types.TRISTE && (imageTriste != null || imageTriste != string.Empty))
        {
            return imageTriste;
        }
        else if (type == Types.CONTENT && (imageContent != null || imageContent != string.Empty))
        {
            return imageContent;
        }
        else if (type == Types.PEUR && (imagePeur != null || imagePeur != string.Empty))
        {
            return imagePeur;
        }
        else if (type == Types.COLERE && (imageColere != null || imageColere != string.Empty))
        {
            return imageColere;
        }
        else
        {
            actuelType = Types.DEFAULT;
            return imageDefault;
        }
    }
    
    public enum Types
    {
        DEFAULT,
        TRISTE,
        CONTENT,
        PEUR,
        COLERE,
    }
    
}

