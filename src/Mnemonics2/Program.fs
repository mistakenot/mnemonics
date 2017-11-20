module Program

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Methods
open Export
open Types
open Expressions

open System
open System.IO
open System.Xml.Serialization
open Structures
open Properties
open Fields

let csContext = 
    new TemplatesExportTemplateContextCSharpContext (
        context = "TypeMember, TypeAndNamespace",
        minimumLanguageVersion = 2.0M)

let templates = new TemplatesExport(family = "Live Templates")

[<EntryPoint>]
let main argv = 
    let allTemplates = seq { 
        // Next, generate templates with no return value
        for (methodSym, methodExprs) in allVoidMethodTemplates do
            yield (methodSym, methodExprs) 

        // First generate templates with a return value
        for (methodSym, methodExprs) in allReturnMethodTemplates do
            yield (methodSym, replaceFixed [Constant ("TYPE", "T")] methodExprs)
            for t in allTypes do
                yield (methodSym + t.Symbol, replaceFixed t.Type methodExprs)
            // Also add entries for generic types without the type parameter specified
            for t in genericTypes do
                let genParameter = replaceFixed [Constant ("TYPE", "T")] t.Type
                yield (methodSym + t.Symbol, replaceFixed genParameter methodExprs)  
        
        // Structure templates
        for structure in allStructureTemplates do
            yield structure 
            
        // Property templates, first with completed types
        for property in allProperties do
            yield property 
        // Untyped generic parameter properties
        for (sym, expr) in untypedProperties do
            for t in genericTypes do
                let genParameter = replaceFixed [Constant ("TYPE", "T")] t.Type
                yield (sym + t.Symbol, replaceFixed genParameter expr)
        // Untyped properties
        for (sym, expr) in untypedProperties do
            yield (sym, replaceFixed [Constant ("TYPE", "T")] expr)
            
        // Typed fields
        for field in typedFields do 
            yield field
        // Untyped generic parameter fields
        for (sym, expr) in untypedFields do
            for t in genericTypes do
                let genParameter = replaceFixed [Constant ("TYPE", "T")] t.Type
                yield (sym + t.Symbol, replaceFixed genParameter expr)
        // Untyped fields
        for (sym, expr) in untypedFields do
            yield (sym, replaceFixed [Constant ("TYPE", "T")] expr) }
        
    let allTemplateExports = 
        Seq.map createMethodExport allTemplates 
        |> Seq.distinctBy (fun t -> t.shortcut)
        |> Seq.sortBy (fun t -> t.shortcut)
        |> Seq.toArray

    templates.Template <- allTemplateExports
    let filename = "CSharpMnemonics.xml"
    File.Delete(filename)
    let t = templates.GetType()
    let xs = new XmlSerializer(t)
    use fs = new FileStream(filename, FileMode.Create, FileAccess.Write)
    xs.Serialize(fs, templates)
    printfn "%A C# templates exported" (templates.Template.Length)
    0 // return an integer exit code
