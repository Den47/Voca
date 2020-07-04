package com.voca.testing.classes;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

public class Tester {

    private List<Item> _vocabulary;
    private List<Item> _currentTest;
    private Item _currentItem;

    public Tester(List<Item> vocabulary) {

        if (vocabulary == null || vocabulary.size() == 0)
            throw new IllegalArgumentException();

        _vocabulary = new ArrayList<>(vocabulary);

        resetTest();
        next();
    }

    public String next() {

        if (_currentTest.size() == 0) {
            resetTest();

            if (_currentTest.size() == 0) {
                return null;
            }
        }

        if (_currentTest.size() == 1) {
            _currentItem = _currentTest.get(0);
            resetTest();
            return getSource();
        }

        int randomIndex = ThreadLocalRandom.current().nextInt(0, _currentTest.size());

        _currentItem = _currentTest.get(randomIndex);
        _currentTest.remove(_currentItem);

        return getSource();
    }

    public void removeCurrent() {
        String currentSource = getSource();
        String currentTranslate = getTranslate();

        if (_currentTest.size() == 1)
            _currentTest.clear();

        for (int i = 0; i < _vocabulary.size(); i++) {
            Item item = _vocabulary.get(i);

            if (item.get_source() == currentSource && item.get_translate() == currentTranslate) {
                _vocabulary.remove(item);
                break;
            }
        }
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

    private void resetTest() {
        _currentTest = new ArrayList<>(_vocabulary);
    }
}
