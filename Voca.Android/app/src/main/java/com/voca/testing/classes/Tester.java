package com.voca.testing.classes;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

public class Tester {

    private List<Item> _vocabulary;
    private Item _currentItem;

    public Tester(List<Item> vocabulary) {

        if (vocabulary == null || vocabulary.size() == 0)
            throw new IllegalArgumentException();

        _vocabulary = new ArrayList<>(vocabulary);
        next();
    }

    public String next() {

        if (_vocabulary.size() == 1) {
            _currentItem = _vocabulary.get(0);
            return getSource();
        }

        Item current;
        do {
            int randomIndex = ThreadLocalRandom.current().nextInt(0, _vocabulary.size());
            current = _vocabulary.get(randomIndex);
        }
        while (_currentItem != null && _currentItem.get_source() == current.get_source());

        _currentItem = current;

        return getSource();
    }

    public Boolean check(String translate) {

        if (translate == null)
            return false;

        String currentTranslate = getTranslate();

        if (currentTranslate == null)
            return false;

        return currentTranslate.compareToIgnoreCase(translate) == 0;
    }

    public String getSource() {

        if (_currentItem == null)
            return null;

        return _currentItem.get_source();
    }

    public String getTranslate() {

        if (_currentItem == null)
            return null;

        return _currentItem.get_translate();
    }
}
