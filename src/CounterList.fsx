#r @"../node_modules/fable-arch/Fable.Arch.dll"
#load "Counter.fsx"
#load "Debug.fsx"
#load "List.fsx"

open Fable.Arch
open Fable.Arch.Html

type Key = System.Guid

type Model =
    { counters: (Key * Counter.Model) list }

type Msg =
    | AddCounter
    | UpdateCounter of Key * Counter.Msg
    | RemoveCounter of Key
    | ResetAll

let uuid (): Key =
    System.Guid.NewGuid()

let initialModel =
    { counters = [] }

let update model msg =
    match Debug.log msg with
    | AddCounter ->
        let counters =
            model.counters @ [ uuid (), Counter.initialModel ]
        { model with counters = counters }
    | RemoveCounter id ->
        let counters =
            model.counters
            |> List.filter (fun (id', _) -> id' <> id)
        { model with counters = counters }
    | UpdateCounter (id, msg) ->
        let counters =
            model.counters
            |> List.map (fun (id', counter) ->
                if id' = id then
                    id', Counter.update counter msg
                else
                    id', counter)
        { model with counters = counters }
    | ResetAll ->
        let counters =
            model.counters
            |> List.map (fun (id, counter) ->
                id, Counter.update counter Counter.Reset)
        { model with counters = counters }

let viewCounter (id, counter) =
    let viewCounter id counter =
        Counter.view counter
        |> Html.map (fun msg -> UpdateCounter (id, msg))
    div []
        [ button
            [ classy "btn btn-danger btn-xs pull-right"
              onMouseClick (fun _ -> RemoveCounter id) ]
            [ span [ classy "glyphicon glyphicon-remove" ] [] ]
          viewCounter id counter ]

let view model =
    div []
       [ div [ classy "page-header" ] [ h1 [] [ text "Counter List" ] ]
         div [] (model.counters |> List.map viewCounter |> List.intersperse (hr []))
         hr []
         div []
           [ button
               [ classy "btn btn-primary"
                 onMouseClick (fun _ -> AddCounter) ]
               [ text "Add Counter" ]
             button
               [ classy "btn btn-danger"
                 onMouseClick (fun _ -> ResetAll) ]
               [ text "Reset All" ] ] ]
