/// The intersperse function takes an element and a list and
/// 'intersperses' that element between the elements of the list.
let intersperse sep ls =
    List.foldBack (fun x -> function
        | [] -> [x]
        | xs -> x::sep::xs) ls []
