#r @"../node_modules/fable-arch/Fable.Arch.dll"

open Fable.Arch.Html

type Model =
    { current: int
      highest: int
      lowest: int }

type Msg =
    | Increment of int
    | Decrement of int
    | Reset

let initialModel: Model =
    { current = 0
      highest = 0
      lowest = 0 }

let update (model: Model) msg =
    let updateModel value =
        { model with
            current = value
            highest = max value model.highest
            lowest = min value model.lowest }
    match msg with
    | Increment i ->
        updateModel (model.current + i)
    | Decrement i ->
        updateModel (model.current - i)
    | Reset ->
        initialModel

type ButtonType =
    | Default
    | Primary
    | Info
    | Warning
    | Danger
    override self.ToString() =
        match self with
        | Default -> "btn-default"
        | Primary -> "btn-primary"
        | Info -> "btn-info"
        | Warning -> "btn-warning"
        | Danger -> "btn-danger"

let btn (buttonType: ButtonType) msg lbl =
    button
        [ classList
            [ "btn", true
              buttonType.ToString(), true ]
          onMouseClick (fun _ -> msg) ]
        [ text lbl ]

let viewHighLow model =
    small []
        [ text "highest: "
          span [ classy "text-success" ] [ text (string model.highest) ]
          text ", "
          text "lowest: "
          span [ classy "text-danger" ] [ text (string model.lowest) ] ]

let viewPageHeader model =
    div []
        [ h2 []
            [ text "Counter "
              viewHighLow model ] ]

let resetBtn =
    div [ classy "pull-right" ] [ btn Danger Reset "RESET" ]

let viewButtonList model =
    div []
        [ btn Warning (Decrement 10) "-10"
          btn Info (Decrement 5) "-5"
          btn Primary (Decrement 1) "-1"
          text (string model.current)
          btn Primary (Increment 1) "+1"
          btn Info (Increment 5) "+5"
          btn Warning (Increment 10) "+10"
          resetBtn ]

let separator =
    hr []

let view (model: Model): DomNode<Msg> =
    div []
        [ viewPageHeader model
          viewButtonList model
          separator
          viewHighLow model ]
