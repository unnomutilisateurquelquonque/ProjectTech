// For more information see https://aka.ms/fsharp-console-apps
open System

let mutable grilleJeu : string[,] = Array2D.zeroCreate 6 7;
let mutable donne = ""
let mutable joueur1 = true
let mutable ligne =0
let mutable colonne =0
let mutable loopligne = true
let mutable loopcolonne = true
let mutable x = 0
let mutable y = 0
let mutable valeurRetour = 0
let mutable tour = true
let mutable partieEncour = true
let mutable dernierJeton :int[] = Array.zeroCreate 2 ;
let mutable distanceG= 0
let mutable distanceD= 0


//affiche la grille de jeu
let AfficherTableau() =
    printfn "| 0   1   2   3   4   5   6 |"
    while loopligne do
        colonne <- 0
        printf "|"
        loopcolonne<- true
        while loopcolonne do
            if grilleJeu[ligne,colonne] = null
                then donne <- " "
                else donne <- grilleJeu[ligne,colonne]
            printf "%s" (" "+donne+" ")
            colonne<-colonne+1
            if colonne >= 7
                then loopcolonne<- false
                else printf "|"
        ligne<-ligne+1
        if ligne >= 6
            then loopligne<- false
        printfn "|"
    //reset les valeurs pour la prochaine utilisation
    loopligne<-true
    ligne<-0


//trouver la case vide la plus base d'une colonne
let TrouverY(colonneChoisie:int) =
    //-1 pour une ligne pleine
    valeurRetour<- -1
    ligne<-5
    while loopligne do
            if grilleJeu[ligne,colonneChoisie] = null
                then valeurRetour<-ligne
                     loopligne<-false
                else 
                     //vérifie si la ligne est pleine
                     if ligne = 0
                     then loopligne<-false
                     else ligne<- ligne-1
    //reset les valeurs pour la prochaine utilisation
    loopligne<-true
    ligne<-0
    valeurRetour

//vérifie si le choix entrer par l'usager est bon
let verificationJoueur()=
    try
        x <- Console.ReadLine() |> int
    with
    | :? SystemException as ex-> 
            printfn"La donnée fourné n'est pas un nombre"
            x <- -1
    if x < 0 || x > 6
        then printfn "Choisiser l'une de ces valeurs 0, 1, 2, 3, 4, 5, 6 entrer votre  nouveau choix"
        else y <-TrouverY(x)
             //ligne pleine ou non
             if y = -1
                then printfn "La colonne est pleine veillez choisir une nouvelle colonne"
                     AfficherTableau()
                else if joueur1
                    then grilleJeu[y,x]<- "X"
                         tour<-false 
                         dernierJeton[0]<-x
                         dernierJeton[1]<-y
                    else grilleJeu[y,x]<- "O"
                         tour<-false
                         dernierJeton[0]<-x
                         dernierJeton[1]<-y           





//tour du joueur
let Jouer() =
    if joueur1 
        then printfn "Tour: Joueur1" 
             printfn "Colone"
             while tour do
                verificationJoueur()
             tour<- true
             joueur1<- false
        else printfn "Tour: Joueur2"
             printfn "Colone"
             while tour do
                verificationJoueur()
             tour<- true
             joueur1<- true

//vérifie la ligne
let Verifierligne()=
    //jeton le plus éloignier sur la droite en bas  
    donne<- grilleJeu[dernierJeton[1],dernierJeton[0]]
    distanceD<-0
    distanceG<-0
    //jeton le plus éloignier sur la droite
    while loopligne do
        if (dernierJeton[0]+distanceD) <= 6
            then
                if grilleJeu[dernierJeton[1],(dernierJeton[0]+distanceD)] = donne
                    then distanceD<- distanceD+1
                    else distanceD<- distanceD-1
                         loopligne<-false
            else loopligne<-false
    loopligne<-true

    //jeton le plus éloignier sur gauche       
    while loopligne do
        if (dernierJeton[0]-distanceG) > 0
            then
                if grilleJeu[dernierJeton[1],dernierJeton[0]-distanceG] = donne
                    then distanceG<- distanceG+1
                    else distanceG<- distanceG-1
                         loopligne<-false
            else loopligne<-false
    loopligne<-true

    //vérifier ligne
    if (distanceD+distanceG+1) >=4
        then partieEncour<- false
             AfficherTableau()
             if joueur1
                then printfn "le joueur 2 a gagnier"
                else printfn "le joueur 1 a gagnier"
//vérifie la ligne
let VerifierDiagonalHB()=
    //jeton le plus éloignier sur droite   
    printf "DernierJeton[0] : "
    printfn "%i" dernierJeton[0]  
    printf "DernierJeton[1] : "
    printfn "%i" dernierJeton[1] 
    printf "Distance droite : "
    printfn "%i" distanceD
    printf "Distance gauche : "
    printfn "%i" distanceG
    donne<- grilleJeu[dernierJeton[1],dernierJeton[0]]
    printfn "%s" donne
    distanceD<-0
    distanceG<-0
    //jeton le plus éloignier sur la droite
    while loopligne do
        if (dernierJeton[0]+distanceD) <= 6 && (dernierJeton[1]+distanceD) <=5
            then
                if grilleJeu[dernierJeton[1]-distanceD,(dernierJeton[0]+distanceD)] = donne
                    then distanceD<- distanceD+1
                    else distanceD<- distanceD-1
                         loopligne<-false
            else loopligne<-false
    loopligne<-true

    //jeton le plus éloignier sur gauche       
    while loopligne do
        if (dernierJeton[0]-distanceG) >= 0 && (dernierJeton[1]-distanceG) >= 0
            then
                if grilleJeu[dernierJeton[1]-distanceG,dernierJeton[0]-distanceG] = donne
                    then distanceG<- distanceG+1
                    else distanceG<- distanceG-1
                         printfn "%i" distanceG
                         loopligne<-false
            else loopligne<-false
    loopligne<-true

    //vérifier ligne
    if (distanceD+distanceG+1) >=4
        then partieEncour<- false
             AfficherTableau()
             if joueur1
                then printfn "le joueur 2 a gagnier"
                else printfn "le joueur 1 a gagnier"



//vérifie si la partie est terminé
let VerifierVictoire() =  
    Verifierligne()
    VerifierDiagonalHB()

    //vérifie si la grille est pleine
    while loopcolonne do
            if grilleJeu[5,colonne] = null
                then loopcolonne<-false
                else 
                if colonne>=6
                   then printfn "partie terminé aucun gagnant"
                        partieEncour<-false
                   else colonne<-colonne+1
    loopcolonne<-true
    colonne<-0





while partieEncour do
    AfficherTableau()
    Jouer()
    VerifierVictoire()


