package com.voca.testing

import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.Menu
import android.view.MenuItem
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.TextView.OnEditorActionListener
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.isVisible
import com.voca.testing.classes.Item
import com.voca.testing.classes.Tester
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.content_main.*
import java.io.IOException
import java.io.InputStreamReader

class MainActivity : AppCompatActivity() {

    private lateinit var tester: Tester
    private var defaultTextColor: Int = 0
    private var correctTextColor: Int = Color.parseColor("#0E8C00")

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        setSupportActionBar(toolbar)

        val items = loadItems()
        if (items.isEmpty()) {
            rootContentPanel.isVisible = false
            return
        }

        nextButton.setOnClickListener {
            sourceText.text = tester.next()
            inputText.text.clear()
            inputText.setTextColor(defaultTextColor)
            inputText.requestFocus()
            setFocus()
        }

        hintButton.setOnClickListener {
            Toast.makeText(this, tester.translate, Toast.LENGTH_SHORT).show()
        }

        inputText.addTextChangedListener(object : TextWatcher {
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {}
            override fun afterTextChanged(p0: Editable?) {
                if (tester.check(p0.toString())) {
                    inputText.setTextColor(correctTextColor)
                } else {
                    inputText.setTextColor(defaultTextColor)
                }
            }
        })

        inputText.setOnEditorActionListener(OnEditorActionListener { _, actionId, _ ->
            if (actionId == EditorInfo.IME_ACTION_DONE) {
                nextButton.performClick()
                return@OnEditorActionListener true
            }
            false
        })

        defaultTextColor = inputText.textColors.defaultColor

        tester = Tester(items)

        sourceText.text = tester.next()
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        // Inflate the menu; this adds items to the action bar if it is present.
        menuInflater.inflate(R.menu.menu_main, menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        return when (item.itemId) {
            R.id.action_add -> {
                val intent = Intent(this, EditActivity::class.java)
                startActivity(intent)
                return true
            }
            R.id.action_remove -> {
                removeFile()
                rootContentPanel.isVisible = false
                return true
            }
            else -> super.onOptionsItemSelected(item)
        }
    }

    private fun loadItems(): List<Item> {
        val lines = readFile()
        val list: ArrayList<Item> = ArrayList()

        for (line in lines) {
            val split = line.split(',')
            if (split.size >= 2)
                list.add(Item(split[0], split[1]))
        }

        return list
    }

    private fun readFile(): List<String> {
        return try {
            val input = openFileInput("voca-words.txt")
            val reader = InputStreamReader(input)

            reader.readLines()

        } catch (ioe: IOException) {
            ioe.printStackTrace()
            ArrayList()
        }
    }

    private fun removeFile() {
        try {
            deleteFile("voca-words.txt")
        } catch (ioe: IOException) {
            ioe.printStackTrace()
        }
    }

    private fun setFocus() {
        val imm: InputMethodManager =
            getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
        imm.toggleSoftInput(
            InputMethodManager.SHOW_FORCED,
            InputMethodManager.HIDE_IMPLICIT_ONLY
        )
    }
}
