module Fields

open Expressions
open Types

type FieldType = 
    | Mutable
    | Readonly

let template = function
    | Mutable -> [Text "private"; space; FixedType; space; fieldName; semiColon]
    | Readonly -> [Text "private readonly"; space; FixedType; space; fieldName; semiColon]

let sym = function
    | Mutable -> "v"
    | Readonly -> "vr"

let untypedFields = seq {
    for t in [Mutable; Readonly] do
        yield (sym t, template t) }

let typedFields = seq {
    for (sym, fieldType) in untypedFields do
        for t in allTypes do
            yield (sym + t.Symbol, replaceFixed t.Type fieldType) }