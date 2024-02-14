# Change log
All notable changes to this project will be documented in this file.

## [v?.?.?] (Unreleased)

## [v2.0.0.1] (2024-02-14)

### Changed
- Hits resource sorting and concurrentbag data structure reordered results issue fixed

## [v2.0.0] (2024-01-30)

### Changed
- Using the new gsearch service from dataforsyningen.dk at https://api.dataforsyningen.dk/rest/gsearch/v2.0
- Response parsing using regex operations to avoid challenge with loading libraries under ArcMap 10.8.1

## [v1.5.0] (2016-10-26)

### Changed
- Now using the stednavne_v3 service instead of stednavne_v2 service.

## [v1.4.0] (2016-07-07)

###  Changed
- Built with .net 4.5 since this is the framework version that is supported by esri.
- The size of the search bar is now adjustable.

## [v1.3.0] (2016-06-09)

### Changed 
- Built with ArcGIS 10.4.1 and .net 4.6.1

## [v1.1.2] (2016-04-26)

### Fix
- Default selected search engines is now changed to `stednavne-v2` only.

## [v1.1.1] (2016-04-26)

### Changed
- Default selected search engines is now changed to `stednavne-v2` only.

## [v1.1.0] (2016-04-13)

### Fix
- Named places can now be selected individually even if they have the same presentation name. 

### Changed
- The dropdown list with search results can contain 1000 items (in stead of 20).

## [v1.0.6] (2015-06-26)
### Fix
- Problem with the mouse being invisable when search result is viewed in the dropdown fixed.

## [v1.0.5] (2015-06-12)
### Added
- Information button has been added to the toolbar.
- It is possible til select with the cursor in the dropdown, and the it zoom to that.
- It is possible til tapdown with the arrow keys in the result dropdown, while zooming to the selected in the list.
- `Ok` button is added to configuration window.

### Fix
- All result is showen from the seachresult in the dropdown

### Changed
- The zoom button is removed.

## [v1.0.4] (2015-06-02)
### Changed
- Downgraded to run in .net version 3.5 rather then 4.5.


## [v1.0.3] (2015-06-02)
### Fix
- Only the version number is changed to se if that fix the problem of deploying a new version.

## [v1.0.2] (2015-06-02)
### Fix
- Problem of searching on text that contain a slash fixed.

- Problem of searching on place name in the new data model, fix by adding search resource 'stednavne_v2'.

## [v1.0.1] (2015-05-26)
### Changed
- The zoom to feature functionality is changed to have a minimum extent in meters

## [v1.0.0] (2015-05-22)
### Added
- The zoom to feature functionality is changed to have a minimum extent.

### Changed
- tooltips and tool naming is improved.

## [v0.3] (2015-05-19)

### Fixed
- Change the name-space to distinguish the dll.

## [v0.2] (2015-05-18)

### Added
- Added possibility for configuration of witch resources to search on.

## [v0.1] (2015-05-13)
- First version of the PlaceFinder.