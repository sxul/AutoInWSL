#!/bin/bash
cd $(dirname $0)

if [ ! -n "$EDITOR" ]; then  
  EDITOR=vi
fi  

$EDITOR commands
