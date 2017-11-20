module DotNet

open Types

let dotNetGenericTypes =
  [
    ("L", "System.Collections.Generic.List", 1)
    ("H",  "System.Collections.Generic.HashSet", 1)
    ("T", "System.Threading.Tasks.Task", 1)
    (*"di.", "System.Collections.Generic.Dictionary", 2*)
    ("E",  "System.Collections.Generic.IEnumerable", 1)
  ]