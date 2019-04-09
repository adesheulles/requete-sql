# requete-sql-c-
Dans ce tp nous avons vu comment charger les données d'une base de donnée et les mettre dans des classes en c#, puis on les a modifiées et on les a sauvegarder dans la BDD.Le projet ce charge en DLL pour être utilisé dans d'autre projet.
 
 Les outils mis en oeuvre :
 * VisualStudio
 
 Le développement tourne autour de 3 grandes parties.
 * Charge les données de la BDD.
 * Affiche ou modifie les données.
 * Sauvegarde les données dans la BDD.
 
 |développement          |langages |technique de programmation                           |
|-----------------------|:-------:|----------------------------------------------------:|
|Charge les données de la BDD |c#|programmation objet|
|Affiche ou modifie les données|c#|programmation objet|
|Sauvegarde les données dans la BDD|c#|programmation objet|
 
Tout d'abord la création des classes qui sont liés à la base de données GESPER :

![Diagramme.png](https://github.com/SamGdy/TpGesperBibliothequeClasse/blob/master/Images/BdGesper.png)

La bibliotheque de classe :

![Diagramme.png](https://github.com/SamGdy/TpGesperBibliothequeClasse/blob/master/Images/DiagrammeDeClasse.png)

Dans l'explorateur de solution :

![Diagramme.png](https://github.com/SamGdy/TpGesperBibliothequeClasse/blob/master/Images/BiblioClasse.PNG)

On va ensuite ajouter la DLL a notre application console comme on peut le voir dans les références :

![Diagramme.png](https://github.com/SamGdy/TpGesperBibliothequeClasse/blob/master/Images/Reference.PNG)

On peut maintenant utiliser la bibliotheque de classe sans recréer de classe dans notre projet.
