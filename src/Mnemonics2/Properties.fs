module Properties

open Expressions
open Types

type PropertyType = 
    | GetSet
    | Get

let template = function
    | GetSet -> [Text "public"; space; FixedType; space; propName; Text "{get; set;}"]
    | Get -> [Text "public"; space; FixedType; space; propName; Text "{get;}"]

let sym = function
    | GetSet -> "p"
    | Get -> "pr"

let untypedProperties = seq {
    for propertyType in [GetSet; Get] do
        yield (sym propertyType, template propertyType) }

let allProperties = seq {
    for (sym, propertyType) in untypedProperties do
        for t in allTypes do
            yield (sym + t.Symbol, replaceFixed t.Type propertyType) }