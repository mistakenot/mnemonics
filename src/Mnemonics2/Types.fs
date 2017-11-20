module Types

open Expressions

type TypeInfo = {
    Symbol: string
    Type: Expression list
    Default: Expression list }

let syst (s,t,d) = {Symbol = s; Type = [Text t]; Default = [Text d]}

// Most commonly used system types
let systemTypes = [
    ("b", "bool", "false")
    ("c", "char", "0")
    ("f", "float", "0.0f")
    ("by", "byte", "0")
    ("i", "int", "0")
    ("m", "decimal", "0M")
    ("s", "string", "\"\"")
    ("g", "System.Guid", "System.Guid.NewGuid()")
    ("dt", "System.DateTime", "System.DateTime.UtcNow")
    ("t", "System.Threading.Tasks.Task", "new System.Threading.Tasks.Task()")] |> Seq.map syst

// Most commonly used generic types
let genericTypes = [
    {   Symbol = "L"
        Type = [Text "System.Collections.Generic.List<"; FixedType; Text ">"]
        Default = [Text "new System.Collections.Generic.List<"; FixedType; Text ">()"] } 
    {   Symbol = "R"
        Type = [FixedType; Text "[]"]
        Default = [Text "new "; FixedType; Text "[]"] } 
    {   Symbol = "E"
        Type = [Text "System.Collections.Generic.IEnumerable<"; FixedType; Text ">"]
        Default = [Text "new System.Collections.Generic.List<"; FixedType; Text ">()"] }
    {   Symbol = "T"
        Type = [Text "System.Threading.Tasks.Task<"; FixedType; Text ">"]
        Default = [Text "new System.Threading.Tasks.Task<"; FixedType; Text ">()"] } ]

let allTypes = seq {
    for s in systemTypes do 
        yield s
    for gt in genericTypes do 
        for syst in systemTypes do
            yield { 
                Symbol = gt.Symbol + syst.Symbol
                Type = replaceFixed syst.Type gt.Type
                Default = replaceFixed syst.Default gt.Default } }