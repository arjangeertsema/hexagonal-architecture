find . -type d -name "Generated" | xargs rm -rf
mkdir Generated
wget https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/process.feature -O ./Generated
