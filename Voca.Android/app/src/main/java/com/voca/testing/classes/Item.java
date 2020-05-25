package com.voca.testing.classes;

public class Item {

    private String _source;
    private String _translate;

    public Item(String source, String translate) {
        _source = source;
        _translate = translate;
    }

    public String get_source() {
        return _source;
    }

    public String get_translate() {
        return _translate;
    }
}
