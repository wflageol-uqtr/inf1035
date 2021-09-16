open System

type Book = { BookNb: int }
type Basket = { Books: Book list }
type Discount = { Nb: int
                  Ratio: float }

module Basket =
    let basket bookNbs =
        { Books = List.map (fun n -> { BookNb = n }) bookNbs }

    let isEmpty basket =
        List.isEmpty basket.Books
        
    let howManyDifferent basket =
        List.distinct basket.Books |> List.length

    let howMany basket book =
        List.filter ((=) book) basket.Books
        |> List.length

module Discount =
    let canBeApplied basket discount =
        Basket.howManyDifferent basket >= discount.Nb

    let apply price discount =
        price * (float discount.Nb) * discount.Ratio
       
    let removeFirstOccurrence books book =
        let rec doRemove acc l =
            match l with
            | [] -> acc
            | (hd::tl) -> if hd = book
                          then List.append acc tl
                          else doRemove (hd::acc) tl
        doRemove [] books
        
    let removePaidBooks basket discount =
        let booksToRemove =
            List.distinct basket.Books
            |> List.sortBy (Basket.howMany basket)
            |> List.rev
            |> List.take discount.Nb
        { Books = List.fold removeFirstOccurrence basket.Books booksToRemove }

module Cashier =
    let availableDiscounts discounts basket =
        List.filter (Discount.canBeApplied basket) discounts

    let rec compute price discounts basket =
        if Basket.isEmpty basket
        then 0.0
        else
          let available = availableDiscounts discounts basket
          match available with
          | [] -> price * (List.length basket.Books |> float)
          | available ->
            List.map (computeDiscount price discounts basket) available
            |> List.min
    and private computeDiscount price discounts basket discount =
        let partialPrice = Discount.apply price discount
        let remaining = Discount.removePaidBooks basket discount
        partialPrice + compute price discounts remaining

[<EntryPoint>]
let main argv =
    let price = 8.0
    let discounts = [{ Nb = 2; Ratio = 0.95}
                     { Nb = 3; Ratio = 0.9}
                     { Nb = 4; Ratio = 0.8}
                     { Nb = 5; Ratio = 0.75}]
    Cashier.compute price discounts (Basket.basket [1; 1; 2; 3])
    |> printfn "Total price: %A"

    0 // return an integer exit code