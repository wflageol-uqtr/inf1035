## Travail Pratique 2 - Poker

Pour votre deuxième travail pratique, vous aurez à restructurer (_refactor_) du code légataire dans une mise en situation. Le programme en question est un algorithme pour analyser les mains de Poker.

### Spécifications

L'algorithme prend en entrée une chaîne de caractères représentant une main de 5 cartes encodées sous la forme suivante :

Valeurs : A 2 3 4 5 6 7 8 9 T J Q K  
Suites : H D S C

Exemple de main : _AS 4C TD QC 4C_

L'algorithme retourne ensuite le type de main en tant que valeur de l'énumération suivante :

    public enum Hand {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
    
Voici une courte explication de chaque main, de la plus faible à la plus forte. Dans le cas où plusieurs types de main s'applique, l'algorithme retourne toujours la plus forte.

| Valeur        | Nom          | Description                                          |
| ---           | ---          | ---                                                  |
| HighCard      | Carte haute  | Aucune autre main visible.                           |
| Pair          | Paire        | Deux cartes de même valeur.                          |
| TwoPair       | Deux paires  | Deux paires différentes.                             |
| ThreeOfAKind  | Brelan       | Trois cartes identiques.                             |
| Straight      | Suite        | Cinq cartes de valeurs successives (e.g. 8-9-T-J-Q). |
| Flush         | Couleur      | Cinq cartes de la même suite.                        |
| FullHouse     | Main pleine  | Un brelan et une paire.                              |
| FourOfAKind   | Carré        | Quatre cartes identiques.                            |
| StraightFlush | Quinte flush | Cinq cartes de valeurs successives de la même suite. |


### À faire

Le code déjà en place satisfait les spécifications présentées. L'objectif ici sera de le restructurer en utilisant de bonnes pratiques de conception et programmation pour ensuite le faire évoluer : certaines fonctionnalités sont à ajouter.

#### Quinte flush royale

En plus des mains présentées ci-haut, le code devra aussi détecter la possibilité d'une quinte flush royale, dont voici la spécification :

| Valeur     | Nom                 | Description                        |
| ---        | ---                 | ---                                |
| RoyalFlush | Quinte flush royale | T, J, Q, K, et A de la même suite. |

Cette main a une valeur supérieure à toutes les autres et doit donc être priorisée.

#### Joker

En second lieu, l'algorithme devra maintenant permettre d'ajouter jusqu'à deux jokers dans une main. Un joker (représenté par l'encodage JK) prend la valeur et la suite de façon à ce que la main ait la valeur la plus élevée possible.

Par exemple, la main _8S JK 6S 5S 4S_ verra le joker prendre la valeur _7S_, car cela compléterait une quinte flush. De même, la main _AH JK JK 4S 7H_ verra les deux jokers prendre la valeur _A_ (ou _4_ ou _6_) pour donner un brelan.

#### Récupération du code

Vous pouvez obtenir le code source à mettre à jour sur GitHub Classroom en utilisant le lien suivant : [https://classroom.github.com/a/gzLRdjgs](https://classroom.github.com/a/gzLRdjgs)

## Critères de correction

Comme nous sommes encore en début de session et que les notions de conception objet n'ont pas encore toutes été vues, la correction se fera en conséquence : je m'attends à du code cohérent qui passe les tests unitaires et respecte les principes SOLID.

Un 20% de la note sera attribué pour la clarté et qualité du code. Cela signifie donner des noms significatifs à vos noms de méthodes et classes, avoir des méthodes simples et compréhensibles (commentaires peuvent parfois aider) et simplement en général être facile à lire.

| Critère                            | Poids |
| ---                                | ---   |
| Les tests unitaires passent        | 60%   |
| Les principes SOLID sont respectés | 30%   |
| Clarté et qualité du code          | 10%   |

## Remise des travaux

Pour remettre votre travail, il suffira de faire un push de vos modifications vers le Github et je pourrai le récupérer à la date de remise. Vous pouvez dès aujourd'hui remettre votre travail, il n'y a pas de date minimale à attendre.

**Les travaux remis passé la date de remise ne seront pas évalués.**
