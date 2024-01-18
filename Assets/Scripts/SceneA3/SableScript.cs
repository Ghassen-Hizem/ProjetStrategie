using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SableScript : MonoBehaviour
{

    private int LowVitesseSoldat;
    private int LowVitesseCavalier;
    private int LowVitesseBouclier;
    private int LowVitesseTirailleur;
    private int LowVitesseMagicien;
    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    public ControlledUnit controlledSoldat;
    public ControlledUnit controlledBouclier;
    public ControlledUnit controlledTirailleur;
    

    void Start()
    {
         LowVitesseSoldat = 2;
         LowVitesseCavalier = 4;
         LowVitesseBouclier = 1;
         LowVitesseTirailleur = 3;
         LowVitesseMagicien = 2;
    
    }


    private void OnTriggerEnter(Collider other) {

        Debug.Log("trigger enter");
       
            if(other.CompareTag("Soldat")) {
                controlledSoldat.speed = LowVitesseSoldat;
            }
            
            else if(other.CompareTag("Cavalier")) {
                controlledCavalier.speed = LowVitesseCavalier;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledBouclier.speed = LowVitesseBouclier;
            }
            else if(other.CompareTag("Tirailleur")) {
                controlledTirailleur.speed = LowVitesseTirailleur;
            }
            else if(other.CompareTag("Magicien")) {
                controlledMagicien.speed = LowVitesseMagicien;
            }
            
        
        
    }
     private void OnTriggerStay(Collider other) {

        Debug.Log("trigger enter");
       
            if(other.CompareTag("Soldat")) {
                controlledSoldat.speed = LowVitesseSoldat;
            }
            else if(other.CompareTag("Cavalier")) {
                controlledCavalier.speed = LowVitesseCavalier;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledBouclier.speed = LowVitesseBouclier;
            }
            else if(other.CompareTag("Tirailleur")) {
                controlledTirailleur.speed = LowVitesseTirailleur;
            }
            else if(other.CompareTag("Magicien")) {
                controlledMagicien.speed = LowVitesseMagicien;
            }
            
       
        
    }
    private void OnTriggerExit(Collider other) {

        Debug.Log("trigger exit");
       
          if(other.CompareTag("Soldat")) {
                controlledSoldat.speed = 3;
            }
           else if(other.CompareTag("Cavalier")) {
                controlledCavalier.speed = 7;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledBouclier.speed = 2;
            }
            else if(other.CompareTag("Tirailleur")) {
                controlledTirailleur.speed = 4;
            }
            else if(other.CompareTag("Magicien")) {
                controlledMagicien.speed = 4;
            }
        
    }
}
