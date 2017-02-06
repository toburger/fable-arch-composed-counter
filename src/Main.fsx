#r @"../node_modules/fable-core/Fable.Core.dll"
#r @"../node_modules/fable-arch/Fable.Arch.dll"
#load "Counter.fsx"
#load "CounterList.fsx"
#load "CounterTuple.fsx"

open Fable.Arch
open Fable.Arch.App
open Fable.Arch.Html

type Model =
    { CounterTuple: CounterTuple.Model
      CounterList: CounterList.Model }

type Msg =
    | UpdateCounterTuple of CounterTuple.Msg
    | UpdateCounterList of CounterList.Msg

let initialModel =
    { CounterTuple = CounterTuple.initialModel
      CounterList = CounterList.initialModel }

let update model msg =
    match msg with
    | UpdateCounterTuple msg ->
        { model with CounterTuple = CounterTuple.update model.CounterTuple msg }
    | UpdateCounterList msg ->
        { model with CounterList = CounterList.update model.CounterList msg }

let view model =
    div [ classy "container" ]
        [ div [ classy "row" ]
            [ div
                [ classy "col-md-6" ]
                [ Html.map UpdateCounterTuple (CounterTuple.view model.CounterTuple) ]
              div
                [ classy "col-md-6" ]
                [ Html.map UpdateCounterList (CounterList.view model.CounterList) ] ] ]

createSimpleApp initialModel view update Virtualdom.createRender
|> withStartNodeSelector "#app"
|> withInitMessage (fun h -> h (UpdateCounterList CounterList.AddCounter))
|> start
