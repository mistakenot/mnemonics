module Structures

open Expressions

type StructureType = 
    | Interface
    | Class
    | AbstractClass
    | StaticClass
    | Struct
    | Enum

let template = function
    | Interface -> [pub; space; interf; space; Text "I"; interfaceName; Scope [endConstant]]
    | Class -> [pub; space; clss; space; className; Scope [endConstant]]
    | AbstractClass -> [pub; space; Text "abstract"; space; clss; space; className; Scope [endConstant]]
    | StaticClass -> [pub; space; Text "static"; space; clss; space; className; Scope [endConstant]]
    | Struct -> [pub; space; Text "struct"; space; className; Scope [endConstant]]
    | Enum -> [pub; space; Text "enum"; space; className; Scope [endConstant]]

let sym = function
    | Interface -> "i"
    | Class -> "c"
    | AbstractClass -> "a"
    | StaticClass -> "C"
    | Struct -> "s"
    | Enum -> "e"

let allStructureTemplates = seq {
    for s in [Interface; Class; AbstractClass; StaticClass; Struct; Enum] do
        yield (sym s, template s) }