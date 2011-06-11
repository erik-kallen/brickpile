﻿/* Copyright (C) 2011 by Marcus Lindblom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BrickPile.Domain;
using BrickPile.UI.Common;

namespace BrickPile.UI.Models {
    public class DefaultPageModel {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [Display(Name = "Name", Prompt = "My awesome page")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the selected page model.
        /// </summary>
        /// <value>
        /// The selected page model.
        /// </value>
        [ScaffoldColumn(false)]
        public string SelectedPageModel { get; set; }
        /// <summary>
        /// Gets the available models.
        /// </summary>
        [Display(Name = "Model", Order = 10)]
        public IEnumerable<SelectListItem> AvailableModels {
            get {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                        foreach (var type in assembly.GetTypes()) {
                        if (type.GetCustomAttributes(typeof(PageModelAttribute), true).Length > 0) {
                            yield return new SelectListItem { Text = type.GetAttribute<PageModelAttribute>().Name, Value = type.AssemblyQualifiedName };
                        }
                    }
                }
            }
        }
    }
}