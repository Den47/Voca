package com.voca.testing

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.inputmethod.EditorInfo
import android.view.inputmethod.InputMethodManager
import android.widget.TextView
import kotlinx.android.synthetic.main.activity_edit.*
import java.io.IOException
import java.io.OutputStreamWriter

class EditActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit)

        addButton.setOnClickListener {

            val a = sourceText.text.toString()
            if (a.isNullOrBlank() || a.isNullOrEmpty())
                return@setOnClickListener

            val b = translateText.text.toString()
            if (b.isNullOrBlank() || b.isNullOrEmpty())
                return@setOnClickListener

            appendToFile(sourceText.text.toString(), translateText.text.toString())

            sourceText.text.clear()
            translateText.text.clear()

            sourceText.requestFocus()
            setFocus()
        }

        sourceText.setOnEditorActionListener(TextView.OnEditorActionListener { _, actionId, _ ->
            if (actionId == EditorInfo.IME_ACTION_DONE) {
                translateText.requestFocus()
                setFocus()
                return@OnEditorActionListener true
            }
            false
        })

        translateText.setOnEditorActionListener(TextView.OnEditorActionListener { _, actionId, _ ->
            if (actionId == EditorInfo.IME_ACTION_DONE) {
                addButton.performClick()
                return@OnEditorActionListener true
            }
            false
        })
    }

    private fun appendToFile(source: String, translate: String) {
        try {
            val output = openFileOutput("voca-words.txt", Context.MODE_APPEND)
            val writer = OutputStreamWriter(output)

            writer.appendln("$source,$translate")
            writer.close()

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
