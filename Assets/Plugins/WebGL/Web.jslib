var Web= {
    OpenUrl: function(str)
    {
	window.open(Pointer_stringify(str));
    }
  
};

mergeInto(LibraryManager.library, Web);