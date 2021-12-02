export VERSION="v0.0.1-alpha"

cd Adapters/Adapters.Rest
bash generate.sh
cd ../..

cd Adapters/Adapters.Zeebe
bash generate.sh
cd ../..

cd Adapters/Adapters.Zeebe.Tests
bash generate.sh
cd ../..