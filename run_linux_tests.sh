#!/bin/bash

echo "(Start) linux tests"
echo "---"

chmod +x ./tests/linux/run.sh
cd tests/linux/
./run.sh

echo "---"
echo "(End) linux tests"
