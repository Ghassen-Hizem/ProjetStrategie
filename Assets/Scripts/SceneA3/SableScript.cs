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
    public ControlledMagicien controlledMagicien;
    public ControlledCavalier controlledCavalier;
    public ControlledSoldat controlledSoldat;
    public ControlledBouclier controlledBouclier;
    public ControlledTirailleur controlledTirailleur;
    public EnemyMagicien controlledEnemyMagicien;
    public EnemyCavalier controlledEnemyCavalier;
    public EnemyBouclier controlledEnemyBouclier;

    void Start()
    {
         LowVitesseSoldat = 2;
         LowVitesseCavalier = 4;
         LowVitesseBouclier = 1;
         LowVitesseTirailleur = 3;
         LowVitesseMagicien = 2;
    
    }

    // je peux pas utiliser l'existence de script pour reduire la vitesse puisque les controller crée impose leur vitesse attribués 
    // du coup , pour les unités qui posséde des controlleurs j'ai changé directement la vitesse du controlleurs
    // et pour les unités sans controlleurs , j'ai changé la vitesse de agent


    private void OnTriggerEnter(Collider other) {

        

        if(other.gameObject.GetComponent<SelectableUnit>() != null) {
            if(other.gameObject.CompareTag("Soldat")) {
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

        else {
            if(other.gameObject.CompareTag("Soldat")) {
                other.gameObject.GetComponent<NavMeshAgent>().speed = LowVitesseSoldat;
            }
            
            else if(other.CompareTag("Cavalier")) {
                controlledEnemyCavalier.speed = LowVitesseCavalier;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledEnemyBouclier.speed = LowVitesseBouclier;
            }
            else if(other.CompareTag("Tirailleur")) {
                other.gameObject.GetComponent<NavMeshAgent>().speed = LowVitesseTirailleur;
            }
            else if(other.CompareTag("Magicien")) {
                controlledEnemyMagicien.speed = LowVitesseMagicien;
            }
        }
       
           
            
        
        
    }
     private void OnTriggerStay(Collider other) {

     

        if(other.gameObject.GetComponent<SelectableUnit>() != null) {
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

        else {
            if(other.gameObject.CompareTag("Soldat")) {
                other.gameObject.GetComponent<NavMeshAgent>().speed = LowVitesseSoldat;
            }
            
            else if(other.CompareTag("Cavalier")) {
                controlledEnemyCavalier.speed = LowVitesseCavalier;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledEnemyBouclier.speed = LowVitesseBouclier;
            }
            else if(other.CompareTag("Tirailleur")) {
                other.GetComponent<NavMeshAgent>().speed = LowVitesseTirailleur;
            }
            else if(other.CompareTag("Magicien")) {
                controlledEnemyMagicien.speed = LowVitesseMagicien;
            }
        }
            
       
        
    }
    private void OnTriggerExit(Collider other) {

       

        if(other.gameObject.GetComponent<SelectableUnit>() != null) {
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

        else {
            if(other.CompareTag("Soldat")) {
                other.gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
            }
            
            else if(other.CompareTag("Cavalier")) {
                controlledEnemyCavalier.speed = 7;
            }
            else if(other.CompareTag("Bouclier")) {
                controlledEnemyBouclier.speed = 2;
            }
            else if(other.CompareTag("Tirailleur")) {
                other.GetComponent<NavMeshAgent>().speed = 4;
            }
            else if(other.CompareTag("Magicien")) {
                controlledEnemyMagicien.speed = 4;
            }
        }
    }
}
