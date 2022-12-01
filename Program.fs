// For more information see https://aka.ms/fsharp-console-apps
open System

let variable =
    let mutable grilleJeu : string[,] = Array2D.zeroCreate 6 7;
    let mutable donne = ""
    let mutable joueur1 = true
    let mutable ligne =0
    let mutable colonne =0
    let mutable loopligne = true
    let mutable loopcolonne = true
    let mutable laGrille = ""
    let mutable x = 0
let afficherGrille =
    if joueur1 = true then printfn "Tour: Joueur1" else printfn "Tour: Joueur1"
    printfn "Colone"
    x <- Console.ReadLine() |> int
    grilleJeu[5,x]<- "X"
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
