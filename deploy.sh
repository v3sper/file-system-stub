NUGET_TOKEN=$1
NUGET_SOURCE=$2
TRAVIS_BRANCH=$3
TRAVIS_BUILD_NUMBER=$4

if [[ $TRAVIS_BRANCH =~ "^fss-*" ]]
then
    SUFFIX = "-alpha"
elif [[ $TRAVIS_BRANCH = "develop" ]]
then
    SUFFIX = "-beta"
elif [[ $TRAVIS_BRANCH =~ "^(master$|release-*)" ]]
then
    SUFFIX = ""
else
	return
fi

.paket/paket pack nuget-files --version "0.0.0-$SUFFIX"