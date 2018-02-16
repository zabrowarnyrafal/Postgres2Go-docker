#!/bin/bash

echo "(Start) linux tests"
echo "---"

sudo chmod +x ./tests/linux/run.sh
./tests/linux/run.sh

echo "---"
echo "(End) linux tests"
