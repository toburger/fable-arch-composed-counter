#r @"../node_modules/fable-core/Fable.Core.dll"

open Fable.Import.Browser

let log x =
    console.log x
    x