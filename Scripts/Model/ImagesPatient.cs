using System;
using Godot;

namespace T3Projet.Tools.Gestion;

public class ImagesPatient
{
    // Liste d'attibute contenant le path de l'image.
    private string imageDefault = null;
    private string imageTriste = null;
    private string imageContent = null;
    private string imagePeur = null;
    private string imageColere = null;

    private Types actuelType;
    public Types ActualType
    {
        get => actuelType;
    }

    public ImagesPatient() { }
    public ImagesPatient(string imageDefault)
    { 
        this.imageDefault = imageDefault;
    }
    
    /// <summary>
    /// Méthode pour ajouter le path de l'image à l'index "index" -> enum value .
    /// </summary>
    /// <param name="image"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public void AjouterImage(string image , int index)
    {
        switch (index)
        {
            case 1:
                this.imageDefault = image;
                break;
            case 2:
                this.imageTriste = image;
                break;
            case 3:
                this.imageContent = image;
                break;
            case 4:
                this.imagePeur = image;
                break;
            case 5:
                this.imageColere = image;
                break;
        }
    }
    /// <summary>
    /// Méthode qui retourne le path de l'image en fonction de l'enum "type".
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Retourne le path de l'image associer à l'énum "type" si il n'existe pas retourne default</returns>
    public string GetImageForEnum(Types type)
    {
        actuelType = type;
        if (type == Types.DEFAULT && (imageDefault != null || imageDefault!= string.Empty))
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
            if (imageDefault != null)
            {
                actuelType = Types.DEFAULT;
                return imageDefault;
            }
            else
            {
                return "";
            }
        }
    }
    
    // Listes d'enum pour choisir l'image du patient.
    public enum Types
    {
        DEFAULT = 1,
        TRISTE = 2,
        CONTENT = 3,
        PEUR = 4,
        COLERE = 4,
    }
    
}

