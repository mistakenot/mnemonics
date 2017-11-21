module Methods

open Expressions

type MethodMode =
    | Instance
    | Static
    | Extension

let methodModes = [Instance; Static; Extension]

type ReturnMode = 
    | SyncVoid
    | SyncResult
    | AsyncVoid
    | AsyncResult

let returnModes = [SyncVoid; SyncResult; AsyncVoid; AsyncResult]

let returnModeTemplate = function
    | SyncVoid -> [Constant ("TYPE", "void")]
    | SyncResult -> [FixedType]
    | AsyncVoid -> [Text "async"; space; Text "Task"]
    | AsyncResult -> [Text "async Task<"; FixedType; Text ">"]

let template methodMode returnMode = 
    match methodMode with
    | Instance -> 
        [pub; space] @ 
        returnModeTemplate returnMode @ 
        [space; methodName; brackets; Scope [endMark]]
    | Static -> 
        [pub; space; stat; space] @ 
        returnModeTemplate returnMode @ 
        [space; methodName; brackets; Scope [endMark]]
    | Extension -> 
        [pub; space; stat; space] @ 
        returnModeTemplate returnMode @ 
        [space; methodName] @ 
        extensionBrackets @  [Scope [endMark]]

let methodModeSym = function
    | Instance -> "m"
    | Static -> "M"
    | Extension -> "X"

let returnModeSym = function
    | SyncVoid -> ""
    | SyncResult -> ""
    | AsyncVoid -> "a"
    | AsyncResult -> "A"

let allVoidMethodTemplates = seq {
    for m in methodModes do
        for r in [SyncVoid; AsyncVoid] do
            yield (methodModeSym m + returnModeSym r, template m r) }

let allReturnMethodTemplates = seq {
    for m in methodModes do
        for r in [SyncResult; AsyncResult] do
            yield (methodModeSym m + returnModeSym r, template m r) }