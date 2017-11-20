module Expressions

type Expression =
    | Text of string // plain text, output as-is
    | FixedType // primary expression type, substituted
    | Variable of string * string // variable, result of LT function eval
    | Constant of string * string // constant, has a default value
    | Scope of Expression list // curly-brace-delimited scope
    | DefaultValue // default value, taken from type enumeration

let rec str = function
    | Text(s) -> s
    | FixedType -> "$fixedtype$"
    | Variable(name, _) -> sprintf "$%s$" name
    | Constant(name, _) -> sprintf "$%s$" name
    | Scope(exprs) -> Seq.map str exprs |> String.concat "" |> sprintf "{%s}"
    | DefaultValue -> "default value?"

let toString = List.map str >> String.concat ""

let getTemplateVariable = function
    | Variable(name, value) -> 
        let v = new TemplatesExportTemplateVariable()
        v.name <- name
        v.initialRange <- 0
        v.expression <- value
        [v]
    | Constant(name, text) when name <> "END" -> 
        let v = new TemplatesExportTemplateVariable()
        v.name <- name
        v.initialRange <- 0
        v.expression <- "constant(\"" + text + "\")"
        [v]
    | _ -> []

let getTemplateVariables = List.collect getTemplateVariable >> List.distinctBy (fun t -> t.name)

let rec replaceFixed fixedType = function
    | FixedType :: tail -> fixedType @ replaceFixed fixedType tail
    | head :: tail -> head :: replaceFixed fixedType tail
    | [] -> []

let space = Text " "
let endConstant = Constant ("END", "")
let semiColon = Text ";"
let lineBreak = Text "\n"
let className = Constant ("CLASSNAME", "MyClass")
let interfaceName = Constant ("INTERFACENAME", "MyInterface")
let traitName = Constant("TRAITNAME", "MyTrait")
let propName = Constant("propname", "MyProperty")
let pub = Text "public"
let stat = Text "static"
let clss = Text "class"
let interf = Text "interface"
let methodName = Constant ("methodname", "MyMethod")
let brackets = Text "()"
let endMark = Constant ("END", "")
let paraTypeMark = Constant ("PTYPE", "TYPE")
let paraValMark = Text "value"
let extensionBrackets = [Text "(this "; paraTypeMark; space; paraValMark; Text ")"]