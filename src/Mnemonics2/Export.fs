module Export

open System
open Expressions

let newId () = Guid.NewGuid().ToString().ToLower()

let csContext = 
  new TemplatesExportTemplateContextCSharpContext (
    context = "TypeMember, TypeAndNamespace",
    minimumLanguageVersion = 2.0M )

let context = new TemplatesExportTemplateContext(CSharpContext = csContext)

let createMethodExport method = 
    let shortcut, template = method
    let variables = getTemplateVariables template
    let t = new TemplatesExportTemplate(shortcut=shortcut)

    t.reformat <- "True"
    t.uid <- newId()
    t.shortenQualifiedReferences <- "True"
    t.Context <- context
    t.text <- toString template
    t.Variables <- List.toArray variables
    t.description <- ""
    t
