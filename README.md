# Monopoly

L'application Monopoly a été réalisée dans le cadre du module POO de la formation du CNAM. Elle a été réalisée en C# avec le framework WPF.

Les règles du jeu

Objectif : Le dernier joueur n’ayant pas fait faillite gagne la partie

## Début du jeu

En début de partie, chaque joueur possèdera une somme totale de : 1.500€ répartie de la façon suivante : 2 x 500€ ; 4 x 100€ ; 1 x 50€ ; 1 x 20€ ; 2 x 10€ ; 1 x 5€ ; 5 x 1€

Les joueurs devront choisir un pion et le placer sur la case de départ.

Le banquier sera géré automatiquement via l’application.

Rôle du banquier et de la banque

Rôle du banquier : (Le rôle du banquier, sera géré automatiquement pas l’application) :

· Il vend les propriétés (sous forme de titres de propriétés correspondants aux terrains).

· Il gère les titres de propriété à vendre.

· Il vend les maisons et les hôtels.

· Il verse les salaires et les primes (remet le salaire au joueur qui passe la case « départ »).

· Il prête de l’argent lors d’une hypothèque.

· Il récolte l’argent des différentes taxes ou amendes mais également des prêts et des intérêts.

## La Banque :

Elle ne fait jamais faillite. Ainsi, il est possible de mettre en circulation autant d’argent que nécessaire sous forme de reconnaissances de dettes écrites sur papier. En effet, Il est possible que la banque n’ait plus de billets lors d’une longue partie. On procédera donc à l’émission de reconnaissance de dettes.

Nombre de billets de la banque :

· 20 x 500 €

· 38 x 100€

· 14 x 50€

· 14 x 20€

· 21 x 10€

· 21 x 5€

· 45 x 1€

Total : 15 140 €

Arrêt sur une propriété à vendre

Si vous vous arrêtez sur une propriété qui n'appartient à personne (c'est-à-dire une propriété dont aucun joueur ne possède le Titre de Propriété), vous pouvez décider de l'acheter. Dans ce cas, payez à la Banque le prix indiqué sur le plateau. La Banque vous donnera alors, en échange, le Titre de Propriété correspondant : placez cette carte, face visible, devant vous. Si vous décidez de ne pas acheter cette propriété, la Banque doit la mettre aussitôt aux enchères, et la vendre au plus offrant. La mise à prix est la première offre faite. Tous les joueurs peuvent participer à l'enchère, y compris le joueur qui a décliné l'achat

## Posséder une propriété

Posséder une Propriété vous donne le droit de percevoir un loyer de la part des joueurs qui vont s'arrêter sur cette Propriété. C'est un atout de posséder les Titres de Propriété de tous les terrains d'une même couleur (autrement dit, de posséder un monopole (car vous pouvez alors construire sur les terrains de ce groupe

## Arrêt sur une propriété privée

Quand vous vous arrêtez sur une Propriété qui a été achetée par un autre joueur, vous pouvez être amené à payer un loyer au Propriétaire, si celui-ci vous réclame ce loyer avant que le joueur jouant à votre suite ait lancé les dés. Le montant du loyer figure sur le Titre de Propriété, et varie selon le nombre de Maisons construits sur cette propriété. Si un joueur possède tous les terrains d'une même couleur, la valeur du loyer de ces terrains est alors multipliée par deux. Par contre, si l'un des terrains du groupe est hypothéquée, le loyer est ordinaire pour les terrains non-hypothéqués, et inexistant pour le terrain hypothéqué. Lorsqu'un joueur a construit des Maisons ou Hôtels sur un terrain, le loyer augmente, comme indiqué sur le Titre de Propriété.

## Arrêt sur les cases “COMPANGIE DE DISTRIBUTION”

Quand vous vous arrêtez sur une de ces cases, vous pouvez l'acheter si cette Compagnie de Distribution n'appartient à personne. Comme pour les Propriétés, payez à la Banque le prix indiqué sur le plateau. Si la Compagnie de Distribution appartient déjà à l'un des joueurs, le propriétaire peut alors vous réclamer un loyer, qui est fonction des points indiqués par les dés que vous venez de lancer :

Si ce propriétaire possède une Compagnie de Distribution, le loyer sera égal à quatre fois la valeur de vos dés S'il possède les deux Compagnies de Distribution, le loyer s'élèvera alors à dix fois la valeur indiquée par les dés. Si vous vous arrêtez sur une de ces cases après avoir suivi les indications d'une

carte Chance ou Caisse de Communauté, vous devrez alors lancer les dés pour déterminer le montant du loyer à payer.

## Arrêt sur une “GARE”
 
Si vous êtes le premier joueur à vous arrêter sur une case Gare, vous pouvez décider de l'acheter. Sinon, cette Gare sera mise aux enchères par la Banque, et même si vous avez décliné l'achat, vous pouvez participer aux enchères. Si la Gare a déjà été achetée, vous devez payer au Propriétaire le montant indiqué sur le Titre de Propriété. Le loyer varie en fonction du nombre total de Gares possédées par ce joueur.

## Arrêt sur une case “CHANCE” ou “COMMUNAUTE”

Lorsque vous vous arrêtez sur l'une de ces cases, vous devez tirer la première carte de la pile indiquée. Ces cartes peuvent vous demander de :

· Déplacer votre pion

· Faire un paiement (par exemple, pour des taxes)

· Recevoir de l'argent

· Aller en Prison

· Sortir de Prison

Vous devez suivre exactement les instructions de cette carte, puis la replacer sous la pile, face cachée. Si vous tirez une carte "VOUS ETES LIBERE DE PRISON", vous pouvez la conserver jusqu'à ce que vous en ayez besoin, ou la vendre, à un prix défini en accord avec l'acheteur.

Remarque : si une carte vous demande de déplacer votre pion, il se peut que vous ayez à passer par la case "Départ". Dans ce cas, recevez 200€ ou 20.000 Francs. Vous ne devez pas passer par la case "Départ" lorsque vous êtes envoyé en Prison.

## Passage sur les cases “IMPOTS” et “TAXE de LUXE”

Payez à la Banque le montant indiqué sur ces cases.

## Arrêt sur le “PARC GRATUIT”

Lorsque vous vous arrêtez sur cette case, vous recevez tout l'argent placé au centre du plateau de jeu. Vous repartirez de cette case au tour suivant.

## PRISON

Vous êtes envoyé en Prison :

· Si vous vous arrêtez sur la case "ALLEZ EN PRISON"

· Si vous tirez une carte Chance ou Caisse de Communauté qui vous indique "ALLEZ EN PRISON"

· Si vous obtenez 3 fois de suite un double avec les dés.

· L'arrivée en Prison met fin à votre tour de jeu. Vous ne franchissez pas la case "Départ" et vous ne recevez pas 20€ ou 20.000 Francs.

Vous pourrez sortir de Prison :

· Si vous payez une amende de 50€ ou 5.000 Francs que vous placez au centre du plateau avant de lancer les dés et de continuer votre tour de jeu, ou

· Si vous faites un double avec les dés durant l'un des trois tours qui suit votre arrivée en Prison ; déplacez-vous alors du nombre de cases indiqué par les dés, jouez, puis relancez les dés - comme tout joueur ayant obtenu un double - et rejouez, ou

· Si vous achetez une carte "VOUS ETES LIBERE DE PRISON" à un autre joueur, à un prix convenu entre vous deux, ou

· Si vous possédez déjà une carte "VOUS ETES LIBERE DE PRISON".

Vous ne pouvez rester plus de trois tours en Prison. Aussitôt après avoir lancé les dés au cours du troisième tour, vous devez - si vous n'avez pas fait de double - payer une amende de 5.000 Francs que vous placez alors au centre du plateau. Puis déplacez-vous du nombre de cases indiqué par les dés et jouez.

Pendant que vous êtes en Prison, vous pouvez percevoir des loyers, si les propriétés correspondantes ne sont pas hypothéquées. Si vous n'êtes pas "envoyé" en Prison mais que vous parvenez sur cette case dans le cours normal du jeu, votre séjour est considéré comme une "simple visite". Vous n'encourrez alors aucune pénalité et vous pouvez vous déplacer normalement au tour suivant.

## Les Maisons

Une fois que vous possédez tous les terrains d'un groupe de couleur, vous pouvez acheter des Maisons à placer sur ces terrains, ce qui augmente la valeur du loyer que vous allez réclamer aux autres joueurs. Le prix d'une Maison est indiqué sur le Titre de Propriété correspondant. À tout moment, durant votre tour de jeu, vous pouvez acheter et construire autant de Maisons que votre fortune vous le permet. Vous devez cependant construire uniformément, c'est-à-dire que vous ne pouvez pas construire plus d'une Maison par terrain tant que vous n'avez pas construit une Maison sur chaque terrain de ce groupe. Vous pouvez alors commencer à construire une seconde rangée de Maisons, et ainsi de suite. De la même façon, si vous souhaitez revendre des Maisons, vous devez dégarnir tous les terrains du groupe uniformément.

Si vous possédez tous les terrains d'un groupe de couleur, et que vous n'avez construit de Maison que sur un ou deux de ces terrains, vous continuez à percevoir un double loyer pour les

terrains nus ce de groupe, lorsqu'un joueur s'y arrête. Vous ne pouvez pas construire de Maison sur un terrain si dans le même groupe, un terrain est hypothéqué

## Les Hôtels

Pour pouvoir acheter un Hôtel, vous devez posséder 4 Maisons sur chaque terrain d'un même groupe. Si vous désirez construire un Hôtel, demandez à la Banque de vous échanger les 4 Maisons du terrain de votre choix contre un Hôtel, et acquittez-vous du prix indiqué pour un Hôtel sur le Titre de Propriété, puis placez votre Hôtel sur ce terrain. Vous ne pouvez construire qu'un Hôtel par terrain.

## Crise du Bâtiment

Si la Banque n'a plus de Maisons à vendre, les joueurs qui désirent construire doivent attendre qu'un joueur ait rendu ou vendu ses Maisons. De la même façon, quand vous vendez des Hôtels, vous ne pouvez pas les remplacer par des Maisons qu'il n'y en a plus de disponibles

## Vente de propriété

Vous pouvez échanger ou vendre des terrains nus, des Gares ou des Compagnies de Distribution avec un autre joueur, à un prix convenu entre vous deux. Cependant, vous ne pouvez pas vendre un terrain à un autre joueur si vous avez une Construction (Maisons ou Hôtel) sur un terrain de même couleur. Pour pouvoir le faire, vous devez auparavant revendre l'ensemble des constructions de ce groupe à la Banque. Une propriété, hypothéquée ou non, ne peut être vendue qu'à un autre joueur, jamais à la Banque. Vous ne pouvez revendre vos Maisons et Hôtels qu'à la Banque. Vous pouvez les vendre à n'importe quel moment du jeu, et à la moitié de leur prix d'achat. Pour un Hôtel, la Banque paiera la moitié du prix d'achat de cet Hôtel plus la moitié du prix d'achat des 4 Maisons qui lui ont été vendus pour la construction de cet Hôtel (soit, en définitive, la moitié du prix d'achat de 5 Maisons sur ce terrain).

## Hypothèques

Si vous avez besoin d'argent, vous pouvez hypothéquer une Propriété, et recevoir de la Banque la somme indiquée au dos du Titre de Propriété.

Les Maisons et les Hôtels ne peuvent pas être hypothéqués. Avant d'hypothéquer un terrain, vous devez donc revendre l'ensemble des constructions situées sur les terrains du même groupe (la Banque les rachète à la moitié du prix d'achat). Vous retournez alors la carte de la propriété hypothéquée, face contre table. Vous ne pouvez plus percevoir de loyer sur cette propriété, ni y construire de Maisons ou d'Hôtels. Vous pouvez lever l'hypothèque, en versant à la Banque la somme qu'elle vous a prêtée, augmentée d'un intérêt de 10%.

Quand vous hypothéquez un terrain, vous en restez propriétaire. Un autre joueur ne peut pas l'acquérir en levant l'hypothèque auprès de la Banque.

Vous pouvez par contre vendre une propriété hypothéquée aux autres joueurs, à un prix que vous aurez défini entre vous. Le nouveau propriétaire a alors le droit de lever cette hypothèque en payant le tarif indiqué, augmenté des 10% d'intérêts. S'il décide de ne pas lever l'hypothèque, il doit néanmoins payer les intérêts. S'il décide de lever l'hypothèque plus tard, il devra repayer les 10% d'intérêts.

## Faillite

Si vous devez à la Banque ou à un autre joueur plus d'argent que vous n'en possédez, vous faites faillite et vous devez alors vous retirer du jeu.

Si votre créancier est la Banque, vous devez lui remettre tout votre argent ainsi que vos Titres de Propriété. Le Banquier met alors aux enchères ces propriétés, qui seront vendues au plus offrant. Vous devez également remettre vos cartes "VOUS ETES LIBERE DE PRISON" sous la pile correspondante. Si votre créancier est un autre joueur, vos Maisons et Hôtels sont vendus à la Banque à la moitié de leur prix d'achat et votre créancier reçoit alors tout l'argent, tous les Titres de Propriété et les cartes "VOUS ETES LIBERE DE PRISON" que vous possédez Si vous possédez des propriétés hypothéquées, vous devez également les remettre à votre créancier, celui-ci doit immédiatement payer les 10% d'intérêt et alors choisir de lever ou non l'hypothèque.

## Remarque :

Si vous devez plus d'argent que vous n'en avez "en espèces", vous pouvez payer votre créancier à la fois en espèces et en propriétés. Dans ce cas, votre créancier peut accepter certaines propriétés (même hypothéquées) pour un montant supérieur à celui qui figure sur le titre de façon à réunir un groupe complet ou à empêcher un adversaire d'obtenir cette propriété.

Si vous êtes Propriétaire, n'oubliez pas de réclamer le paiement de vos loyers. Seule la Banque peut prêter de l'argent, et ce, seulement contre une hypothèque. Un joueur ne peut ni emprunter ni prêter de l'argent à un autre joueur.

Les éléments composant le jeu sont :

· 1 plateau de jeu.

· 8 pions.

· 16 cartes « CAISSE DE COMMUNAUTE ».

· 16 cartes « CHANCE ».

· 28 cartes de propriété.

· 1 paquet de billets de banque de différentes valeurs pour Monopoly.

· 32 maisons vertes.

· 12 hôtels rouges.

· 2 dés.

# ÉNONCÉ DES BESOINS :

## Description :

Développer une application dématérialisée et portable du célèbre jeu de société “Monopoly”

## Contraintes Fonctionnelles :

Développer l’intégralité des fonctionnalités présentes dans le jeu classique du “Monopoly”. Puis y ajouter de nouvelles fonctionnalités comme par exemple :

· La personnalisation du thème et des couleurs de jeux (Classique, Sombre, Modern, Rose, ...)

· Implémenté un mode multijoueur (en LAN ou avec des Intelligence Artificielle).

## Contraintes Techniques :

L’application “Monopoly” sera développée en WPF avec la technologie .NET et les langages C#, XAML et XML.

## Contraintes Organisationnelles :

Réalisation du projet par groupe de 2 personnes : Heures de travail estimées : 4 heures par Semaine (8heures toutes les 2 semaines)

## Ébauches visuelles du jeu :

Au lancement de l’application, une interface de type “menu” apparaît. Sur celle-ci plusieurs boutons seront disponibles : “Jouer seul”, “Multijoueur”, “Personnaliser”, “Règles du jeu”, etc. Un personnage fictif sera créé dès le lancement du jeu, en saisissant son pseudo et en choisissant sa couleur de pion pour afin de rendre le jeu plus ludique.