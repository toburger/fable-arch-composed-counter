#load "Counter.fsx"

open Fable.Arch
open Fable.Arch.Html

type Model =
    { Top: Counter.Model
      Bottom: Counter.Model }

type Msg =
    | UpdateTopCounter of Counter.Msg
    | UpdateBottomCounter of Counter.Msg

let initialModel =
    { Top = Counter.initialModel
      Bottom = Counter.initialModel }

let update model msg =
    match msg with
    | UpdateTopCounter msg ->
        { model with Top = Counter.update model.Top msg }
    | UpdateBottomCounter msg ->
        { model with Bottom = Counter.update model.Bottom msg }

let view model =
    div []
        [ div [ classy "page-header" ] [ h1 [] [ text "Counter Tuple" ] ]
          Html.map UpdateTopCounter (Counter.view model.Top)
          hr []
          Html.map UpdateBottomCounter (Counter.view model.Bottom) ]
