#!/bin/sh
NUGET_TOKEN=$1
TRAVIS_BRANCH=$2
TRAVIS_BUILD_NUMBER=$3

MASTER_OR_RELEASE_BRANCH_REGEX="^(master$|release[-]{1}.*)"
DEVELOP_REGEX="^develop$"
SHORT_LIVED_BRANCH_FSS_REGEX="^fss[-]{1}.*"

if [[ $TRAVIS_BRANCH =~ $SHORT_LIVED_BRANCH_FSS_REGEX ]]
then
    echo "Creating package for short lived branch (alpha)"
    SUFFIX="-alpha"
elif [[ $TRAVIS_BRANCH =~ $DEVELOP_REGEX ]]
then
    echo "Creating package for develop branch (beta)"
    SUFFIX="-beta"
elif [[ $TRAVIS_BRANCH =~ $MASTER_OR_RELEASE_BRANCH_REGEX ]]
then
    echo "Creating package for master/release branch"
    SUFFIX=""
else
	exit 0
fi

MAJOR_MINOR=$(head -n 1 "version")
VERSION="$MAJOR_MINOR.$TRAVIS_BUILD_NUMBER$SUFFIX"
echo "Deploying package in version $VERSION"

.paket/paket pack nuget-files --version $VERSION

.paket/paket push --endpoint https://nuget.org/ --api-key $NUGET_TOKEN nuget-files/*.nupkg
