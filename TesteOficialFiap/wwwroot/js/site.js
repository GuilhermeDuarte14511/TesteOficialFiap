document.addEventListener('DOMContentLoaded', function () {
    const themeToggleBtn = document.getElementById('themeToggleBtn');
    const body = document.body;
    const navLinks = document.querySelectorAll('.nav-link');
    const navBar = document.querySelector('.navbar');
    const footer = document.querySelector('.footer');
    const formControls = document.querySelectorAll('.form-control');
    const labels = document.querySelectorAll('label');
    const buttons = document.querySelectorAll('.btn');
    const toasts = document.querySelectorAll('.toast');
    const modals = document.querySelectorAll('.modal-content');
    const agGrids = document.querySelectorAll('.ag-theme-alpine');

    // Função para aplicar modo escuro
    function applyDarkMode() {
        body.classList.add('dark-mode');
        if (navBar) navBar.classList.add('dark-mode');
        if (footer) footer.classList.add('dark-mode');

        navLinks.forEach(link => {
            link.classList.add('dark-mode');
        });

        formControls.forEach(control => {
            control.classList.add('dark-mode');
        });

        labels.forEach(label => {
            label.classList.add('dark-mode');
        });

        buttons.forEach(button => {
            button.classList.add('dark-mode');
        });

        toasts.forEach(toast => {
            toast.classList.add('dark-mode');
        });

        modals.forEach(modal => {
            modal.classList.add('dark-mode');
        });

        agGrids.forEach(grid => {
            grid.classList.add('ag-theme-alpine-dark');
        });

        themeToggleBtn.textContent = 'Modo Claro';
        localStorage.setItem('darkMode', 'enabled');
    }

    // Função para remover modo escuro
    function removeDarkMode() {
        body.classList.remove('dark-mode');
        if (navBar) navBar.classList.remove('dark-mode');
        if (footer) footer.classList.remove('dark-mode');

        navLinks.forEach(link => {
            link.classList.remove('dark-mode');
        });

        formControls.forEach(control => {
            control.classList.remove('dark-mode');
        });

        labels.forEach(label => {
            label.classList.remove('dark-mode');
        });

        buttons.forEach(button => {
            button.classList.remove('dark-mode');
        });

        toasts.forEach(toast => {
            toast.classList.remove('dark-mode');
        });

        modals.forEach(modal => {
            modal.classList.remove('dark-mode');
        });

        agGrids.forEach(grid => {
            grid.classList.remove('ag-theme-alpine-dark');
        });

        themeToggleBtn.textContent = 'Modo Escuro';
        localStorage.setItem('darkMode', 'disabled');
    }

    // Carregar estado do modo escuro ao carregar a página
    if (localStorage.getItem('darkMode') === 'enabled') {
        applyDarkMode();
    }

    if (themeToggleBtn) {
        themeToggleBtn.addEventListener('click', function () {
            if (body.classList.contains('dark-mode')) {
                removeDarkMode();
            } else {
                applyDarkMode();
            }
        });
    }

    // Outras funções e variáveis comuns (mantidas conforme necessário)
    const quickFilterInput = document.getElementById('quickFilterInput');
    const confirmModalElement = document.getElementById('confirmModal');
    const confirmModal = confirmModalElement ? new bootstrap.Modal(confirmModalElement) : null;
    const confirmMessage = document.getElementById('confirmMessage');
    const confirmActionBtn = document.getElementById('confirmActionBtn');
    const loadingText = document.getElementById('loadingText');
    let currentAction = null;

    const showLoading = () => {
        if (loadingText) loadingText.style.display = 'inline';
    };

    const hideLoading = () => {
        if (loadingText) loadingText.style.display = 'none';
    };

    const showToast = (message, isError) => {
        const toastElement = document.getElementById('toastMessage');
        const toastBody = document.getElementById('toastBody');
        if (toastBody) toastBody.textContent = message;
        if (toastElement) {
            toastElement.className = 'toast show'; // Reset className
            toastElement.classList.add(isError ? 'text-bg-danger' : 'text-bg-success');
            const toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 3000 });
            toast.show();
        }
    };


    const showModal = (message, action) => {
        if (confirmMessage) confirmMessage.textContent = message;
        currentAction = action;
        if (confirmModal) confirmModal.show();
    };

    if (confirmActionBtn) {
        confirmActionBtn.addEventListener('click', function () {
            if (currentAction) {
                currentAction();
                if (confirmModal) confirmModal.hide();
            }
        });
    }

    const localeText = {
        // Traduções para português
        page: "Página",
        more: "Mais",
        to: "para",
        of: "de",
        next: "Próximo",
        last: "Último",
        first: "Primeiro",
        previous: "Anterior",
        loadingOoo: "Carregando...",
        selectAll: "Selecionar tudo",
        searchOoo: "Procurar...",
        blanks: "Em branco",
        filterOoo: "Filtrar...",
        applyFilter: "Aplicar filtro...",
        equals: "Igual a",
        notEqual: "Diferente de",
        lessThan: "Menor que",
        greaterThan: "Maior que",
        lessThanOrEqual: "Menor ou igual a",
        greaterThanOrEqual: "Maior ou igual a",
        inRange: "No intervalo",
        contains: "Contém",
        notContains: "Não contém",
        startsWith: "Começa com",
        endsWith: "Termina com",
        // Os campos de colunas padrão
        andCondition: "E",
        orCondition: "OU",
        group: "Grupo",
        columns: "Colunas",
        filters: "Filtros",
        rowGroupColumnsEmptyMessage: "Arraste colunas para agrupar",
        valueColumnsEmptyMessage: "Arraste colunas para agregar",
        pivotMode: "Modo Pivô",
        groups: "Grupos",
        values: "Valores",
        pivots: "Pivôs",
        valueColumns: "Colunas de valores",
        pivotColumns: "Colunas de pivôs",
        groupColumns: "Colunas de grupo",
        toolPanelButton: "Painel de Ferramentas",
        noRowsToShow: "Sem linhas para mostrar",
        // Outros
        pinColumn: "Fixar Coluna",
        valueAggregation: "Agregação de valor",
        autosizeThiscolumn: "Auto ajustar esta coluna",
        autosizeAllColumns: "Auto ajustar todas colunas",
        groupBy: "Agrupar por",
        ungroupBy: "Desagrupar por",
        resetColumns: "Redefinir Colunas",
        expandAll: "Expandir tudo",
        collapseAll: "Colapsar tudo",
        toolPanel: "Painel de ferramentas",
        export: "Exportar",
        csvExport: "Exportar para CSV",
        excelExport: "Exportar para Excel",
        pinLeft: "Fixar à esquerda",
        pinRight: "Fixar à direita",
        noPin: "Sem fixação",
        sum: "Soma",
        min: "Mín",
        max: "Máx",
        none: "Nenhum",
        count: "Contar",
        average: "Média",
        copy: "Copiar",
        ctrlC: "Ctrl+C",
        paste: "Colar",
        ctrlV: "Ctrl+V"
    };

    // Código específico para a página de Alunos
    if (document.querySelector('#myGrid')) {
        const saveAddAlunoBtn = document.getElementById('saveAddAlunoBtn');
        const saveEditAlunoBtn = document.getElementById('saveEditAlunoBtn');
        const addAlunoBtn = document.getElementById('addAlunoBtn');
        const addSenhaInput = document.getElementById('addSenha');
        const addSenhaFeedback = document.getElementById('addSenhaFeedback');
        const addSenhaStrength = document.getElementById('addSenhaStrength');
        const lengthRequirement = document.getElementById('lengthRequirement');
        const uppercaseRequirement = document.getElementById('uppercaseRequirement');
        const specialCharRequirement = document.getElementById('specialCharRequirement');
        let alunoToEdit = null;
        let selectedTurmas = [];

        const loadTurmas = () => {
            showLoading();
            console.log('Carregando turmas...');
            fetch('/Turma/GetAll')
                .then(response => response.json())
                .then(turmas => {
                    console.log('Turmas carregadas:', turmas);
                    const turmasArray = turmas.map(turma => ({
                        label: `${turma.id} - ${turma.nome}`,
                        value: turma.id
                    }));

                    const autocompleteOptions = {
                        source: turmasArray,
                        appendTo: null,
                        select: function (event, ui) {
                            // Verificação para evitar duplicação
                            if (selectedTurmas.some(t => t.value === ui.item.value)) {
                                showToast(`A turma ${ui.item.label} já foi adicionada.`, true);
                                $(this).val('');
                                return false;
                            }
                            selectedTurmas.push(ui.item);
                            $(this).val('');
                            renderSelectedTurmas(this.id === 'addAutocompleteTurmas' ? 'add' : 'edit');
                            return false;
                        }
                    };

                    $('#addAutocompleteTurmas').autocomplete(autocompleteOptions);
                    autocompleteOptions.appendTo = '#editAlunoModal';
                    $('#editAutocompleteTurmas').autocomplete(autocompleteOptions);

                    hideLoading();
                })
                .catch(error => {
                    console.error('Erro ao carregar turmas:', error);
                    hideLoading();
                });
        };

        const renderSelectedTurmas = (type) => {
            const containerId = type === 'add' ? 'addSelectedTurmasList' : 'editSelectedTurmasList';
            const container = document.getElementById(containerId);
            container.innerHTML = '';
            selectedTurmas.forEach((turma, index) => {
                const turmaElement = document.createElement('div');
                turmaElement.classList.add('selected-turma');
                turmaElement.innerHTML = `
                    <span>${turma.label}</span>
                    <button type="button" class="btn btn-sm btn-danger" onclick="removeTurma(${index}, '${type}')">Remover</button>
                `;
                container.appendChild(turmaElement);
            });
        };

        window.removeTurma = (index, type) => {
            selectedTurmas.splice(index, 1);
            renderSelectedTurmas(type);
        };

        const clearAddModalFields = () => {
            document.getElementById('addNome').value = '';
            document.getElementById('addEmail').value = '';
            document.getElementById('addSenha').value = '';
            document.getElementById('addAtivo').checked = false;
            document.getElementById('addAutocompleteTurmas').value = '';
            if (addSenhaFeedback) addSenhaFeedback.style.display = 'block'; // Mostrar feedback de senha
            validateAddSenha(); // Chamar validação para mostrar as regras
            if (saveAddAlunoBtn) saveAddAlunoBtn.disabled = false;
            selectedTurmas = [];
            renderSelectedTurmas('add');
        };

        const clearEditModalFields = () => {
            document.getElementById('editNome').value = '';
            document.getElementById('editEmail').value = '';
            document.getElementById('editAtivo').checked = false;
            document.getElementById('editAutocompleteTurmas').value = '';
            if (saveEditAlunoBtn) saveEditAlunoBtn.disabled = false;
            selectedTurmas = [];
            renderSelectedTurmas('edit');
            alunoToEdit = null;
        };

        const validateAddSenha = () => {
            const senha = addSenhaInput.value;
            const lengthValid = senha.length >= 8;
            const uppercaseValid = /[A-Z]/.test(senha);
            const specialCharValid = /[!@#$%^&*(),.?":{}|<>]/.test(senha);

            lengthRequirement.style.color = lengthValid ? 'green' : 'red';
            uppercaseRequirement.style.color = uppercaseValid ? 'green' : 'red';
            specialCharRequirement.style.color = specialCharValid ? 'green' : 'red';

            const isValid = lengthValid && uppercaseValid && specialCharValid;
            addSenhaStrength.textContent = isValid ? '' : 'Senha muito fraca. Necessário: ';
            addSenhaStrength.textContent += !lengthValid ? 'Pelo menos 8 caracteres. ' : '';
            addSenhaStrength.textContent += !uppercaseValid ? 'Pelo menos uma letra maiúscula. ' : '';
            addSenhaStrength.textContent += !specialCharValid ? 'Pelo menos um caractere especial. ' : '';

            saveAddAlunoBtn.disabled = !isValid;
        };

        if (addSenhaInput) addSenhaInput.addEventListener('input', validateAddSenha);

        // Configuração do AG Grid para Alunos
        const columnDefs = [
            { headerName: "Nome", field: "nome", sortable: true, filter: true, minWidth: 150 },
            { headerName: "Email", field: "email", sortable: true, filter: true, minWidth: 200 },
            { headerName: "Ativo", field: "ativo", sortable: true, filter: true, minWidth: 100 },
            { headerName: "Turma", field: "turmaNome", sortable: true, filter: true, minWidth: 150 },
            {
                headerName: "Ações", field: "acoes", minWidth: 350, cellRenderer: function (params) {
                    return `
                        <button class="btn btn-sm btn-warning edit-btn" data-id="${params.data.id}">Editar</button>
                        <button class="btn btn-sm btn-danger delete-btn" data-id="${params.data.id}" data-nome="${params.data.nome}">Excluir</button>
                        <button class="btn btn-sm btn-secondary inactivate-btn" data-id="${params.data.id}" style="${params.data.ativo ? '' : 'display:none;'}">Inativar</button>
                        <button class="btn btn-sm btn-success activate-btn" data-id="${params.data.id}" style="${params.data.ativo ? 'display:none;' : ''}">Ativar</button>
                    `;
                }
            }
        ];

        const gridOptions = {
            columnDefs: columnDefs,
            rowData: null,
            animateRows: true,
            pagination: true,
            paginationPageSize: 10,
            domLayout: 'autoHeight',
            localeText: localeText
        };

        const gridDiv = document.querySelector('#myGrid');
        const gridApi = agGrid.createGrid(gridDiv, gridOptions);

        fetch('/Aluno/GetAll')
            .then(response => response.json())
            .then(data => {
                gridApi.setRowData(data);
            })
            .catch(error => {
                console.error("Erro ao carregar dados:", error);
            });

        if (quickFilterInput) {
            quickFilterInput.addEventListener('input', function () {
                gridApi.setQuickFilter(quickFilterInput.value);
            });
        }

        // Eventos de clique para editar, excluir, inativar
        gridDiv.addEventListener('click', function (e) {
            const target = e.target;
            const alunoId = target.getAttribute('data-id');
            if (target.classList.contains('edit-btn')) {
                fetch(`/Aluno/GetAluno/${alunoId}`)
                    .then(response => response.json())
                    .then(aluno => {
                        console.log('Aluno carregado:', aluno);
                        document.getElementById('editNome').value = aluno.nome;
                        document.getElementById('editEmail').value = aluno.email;
                        document.getElementById('editAtivo').checked = aluno.ativo;
                        document.getElementById('editAutocompleteTurmas').value = '';
                        selectedTurmas = aluno.alunoTurmas.map(turma => ({ label: `${turma.turmaId} - ${turma.turma.nome}`, value: turma.turmaId }));
                        renderSelectedTurmas('edit');
                        document.getElementById('editAlunoModalLabel').innerText = 'Editar Aluno';

                        alunoToEdit = alunoId;
                        $('#editAlunoModal').modal('show');
                    })
                    .catch(error => {
                        console.error('Erro ao buscar detalhes do aluno:', error);
                    });
            } else if (target.classList.contains('delete-btn')) {
                showModal(`Tem certeza de que deseja excluir o aluno ${target.getAttribute('data-nome')}?`, () => {
                    fetch(`/Aluno/DeleteAluno/${alunoId}`, { method: 'POST' })
                        .then(response => response.json())
                        .then(result => {
                            if (result.success) {
                                showToast(result.message, false);
                                fetch('/Aluno/GetAll')
                                    .then(response => response.json())
                                    .then(data => {
                                        gridApi.setRowData(data);
                                    });
                            } else {
                                showToast(result.message, true);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao excluir aluno:', error);
                            showToast('Erro ao excluir aluno.', true);
                        });
                });
            } else if (target.classList.contains('inactivate-btn')) {
                showModal('Tem certeza de que deseja inativar este aluno?', () => {
                    fetch(`/Aluno/InativarAluno/${alunoId}`, { method: 'POST' })
                        .then(response => response.json())
                        .then(result => {
                            if (result.success) {
                                showToast(result.message, false);
                                fetch('/Aluno/GetAll')
                                    .then(response => response.json())
                                    .then(data => {
                                        gridApi.setRowData(data);
                                    });
                            } else {
                                showToast(result.message, true);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao inativar aluno:', error);
                            showToast('Erro ao inativar aluno.', true);
                        });
                });
            } else if (target.classList.contains('activate-btn')) {
                showModal('Tem certeza de que deseja ativar este aluno?', () => {
                    fetch(`/Aluno/AtivarAluno/${alunoId}`, { method: 'POST' })
                        .then(response => response.json())
                        .then(result => {
                            if (result.success) {
                                showToast(result.message, false);
                                fetch('/Aluno/GetAll')
                                    .then(response => response.json())
                                    .then(data => {
                                        gridApi.setRowData(data);
                                    });
                            } else {
                                showToast(result.message, true);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao ativar aluno:', error);
                            showToast('Erro ao ativar aluno.', true);
                        });
                });
            }
        });

        if (saveAddAlunoBtn) {
            saveAddAlunoBtn.addEventListener('click', function () {
                const aluno = {
                    nome: document.getElementById('addNome').value,
                    email: document.getElementById('addEmail').value,
                    senha: document.getElementById('addSenha').value,
                    ativo: document.getElementById('addAtivo').checked,
                    alunoTurmas: selectedTurmas.map(turma => ({ turmaId: turma.value })) // Remover alunoId
                };

                console.log("Payload enviado:", aluno); // Log para depuração

                fetch('/Aluno/AddAluno', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(aluno)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            showToast(result.message, false);
                            $('#addAlunoModal').modal('hide');
                            fetch('/Aluno/GetAll')
                                .then(response => response.json())
                                .then(data => {
                                    gridApi.setRowData(data);
                                });
                        } else {
                            showToast(result.message, true);
                            console.error("Erros de validação:", result.errors); // Log para erros de validação
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao salvar aluno:', error);
                        showToast('Erro ao salvar aluno.', true);
                    });
            });
        }

        // Evento de clique para salvar alterações de aluno
        if (saveEditAlunoBtn) {
            saveEditAlunoBtn.addEventListener('click', function () {
                const aluno = {
                    id: alunoToEdit,
                    nome: document.getElementById('editNome').value,
                    email: document.getElementById('editEmail').value,
                    ativo: document.getElementById('editAtivo').checked,
                    alunoTurmas: selectedTurmas.map(turma => ({ turmaId: turma.value }))
                };

                const url = '/Aluno/EditAluno';

                console.log("Dados do aluno:", JSON.stringify(aluno)); // Adicionado log

                fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(aluno)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            console.log(result.message);
                            showToast(result.message, false);
                            $('#editAlunoModal').modal('hide');
                            fetch('/Aluno/GetAll')
                                .then(response => response.json())
                                .then(data => {
                                    gridApi.setRowData(data);
                                });
                        } else {
                            console.error(result.message);
                            showToast(result.message, true);
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao salvar alterações:', error);
                        showToast('Erro ao salvar alterações.', true);
                    });
            });
        }

        // Limpar modais ao fechar e garantir que regras de senha sejam mostradas ao abrir
        $('#addAlunoModal').on('hidden.bs.modal', clearAddModalFields);
        $('#addAlunoModal').on('shown.bs.modal', () => {
            if (addSenhaFeedback) addSenhaFeedback.style.display = 'block'; // Mostrar feedback de senha ao abrir
            validateAddSenha(); // Chamar validação para mostrar as regras
        });
        $('#editAlunoModal').on('hidden.bs.modal', clearEditModalFields);

        // Carregar turmas ao focar no campo de input das turmas
        $('#addAutocompleteTurmas').on('focus', loadTurmas);
        $('#editAutocompleteTurmas').on('focus', loadTurmas);
    }

    // Código específico para a página de Turmas
    if (document.querySelector('#turmaGrid')) {
        const addTurmaBtn = document.getElementById('addTurmaBtn');
        const saveCriarTurmaBtn = document.getElementById('saveCriarTurmaBtn');
        const saveEditarTurmaBtn = document.getElementById('saveEditarTurmaBtn');
        let turmaToEdit = null;

        const clearAddTurmaModalFields = () => {
            document.getElementById('nomeCriarTurma').value = '';
            document.getElementById('ativoCriarTurma').checked = false;
            if (saveCriarTurmaBtn) saveCriarTurmaBtn.disabled = false;
        };

        const clearEditTurmaModalFields = () => {
            document.getElementById('nomeEditarTurma').value = '';
            document.getElementById('ativoEditarTurma').checked = false;
            if (saveEditarTurmaBtn) saveEditarTurmaBtn.disabled = false;
            turmaToEdit = null;
        };

        // Configuração do AG Grid para Turmas
        const turmaColumnDefs = [
            { headerName: "Nome", field: "nome", sortable: true, filter: true, minWidth: 150 },
            { headerName: "Ativo", field: "ativo", sortable: true, filter: true, minWidth: 100 },
            { headerName: "Número de Alunos", field: "numeroAlunos", sortable: true, filter: true, minWidth: 150 },
            {
                headerName: "Ações", field: "acoes", minWidth: 4, cellRenderer: function (params) {
                    return `
                        <button class="btn btn-sm btn-warning edit-turma-btn" data-id="${params.data.id}">Editar</button>
                        <button class="btn btn-sm btn-secondary inactivate-turma-btn" data-id="${params.data.id}" style="${params.data.ativo ? '' : 'display:none;'}">Inativar</button>
                        <button class="btn btn-sm btn-success activate-turma-btn" data-id="${params.data.id}" style="${params.data.ativo ? 'display:none;' : ''}">Ativar</button>
                    `;
                }
            }
        ];

        const turmaGridOptions = {
            columnDefs: turmaColumnDefs,
            rowData: null,
            animateRows: true,
            pagination: true,
            paginationPageSize: 10,
            domLayout: 'autoHeight',
            localeText: localeText
        };

        const turmaGridDiv = document.querySelector('#turmaGrid');
        const turmaGridApi = agGrid.createGrid(turmaGridDiv, turmaGridOptions);

        fetch('/Turma/GetAll')
            .then(response => response.json())
            .then(data => {
                turmaGridApi.setRowData(data);
            })
            .catch(error => {
                console.error("Erro ao carregar dados:", error);
            });

        if (quickFilterInput) {
            quickFilterInput.addEventListener('input', function () {
                turmaGridApi.setQuickFilter(quickFilterInput.value);
            });
        }

        // Eventos de clique para editar, inativar
        turmaGridDiv.addEventListener('click', function (e) {
            const target = e.target;
            const turmaId = target.getAttribute('data-id');
            if (target.classList.contains('edit-turma-btn')) {
                fetch(`/Turma/GetTurma/${turmaId}`)
                    .then(response => response.json())
                    .then(turma => {
                        document.getElementById('nomeEditarTurma').value = turma.nome;
                        document.getElementById('ativoEditarTurma').checked = turma.ativo;
                        turmaToEdit = turmaId;
                        $('#editarTurmaModal').modal('show');
                    })
                    .catch(error => {
                        console.error('Erro ao buscar detalhes da turma:', error);
                    });
            } else if (target.classList.contains('inactivate-turma-btn')) {
                showModal('Tem certeza de que deseja inativar esta turma?', () => {
                    fetch(`/Turma/InativarTurma/${turmaId}`, { method: 'POST' })
                        .then(response => response.json())
                        .then(result => {
                            if (result.success) {
                                showToast(result.message, false);
                                fetch('/Turma/GetAll')
                                    .then(response => response.json())
                                    .then(data => {
                                        turmaGridApi.setRowData(data);
                                    });
                            } else {
                                showToast(result.message, true);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao inativar turma:', error);
                            showToast('Erro ao inativar turma.', true);
                        });
                });
            } else if (target.classList.contains('activate-turma-btn')) {
                showModal('Tem certeza de que deseja ativar esta turma?', () => {
                    fetch(`/Turma/AtivarTurma/${turmaId}`, { method: 'POST' })
                        .then(response => response.json())
                        .then(result => {
                            if (result.success) {
                                showToast(result.message, false);
                                fetch('/Turma/GetAll')
                                    .then(response => response.json())
                                    .then(data => {
                                        turmaGridApi.setRowData(data);
                                    });
                            } else {
                                showToast(result.message, true);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao ativar turma:', error);
                            showToast('Erro ao ativar turma.', true);
                        });
                });
            }
        });

        if (saveCriarTurmaBtn) {
            saveCriarTurmaBtn.addEventListener('click', function () {
                const turma = {
                    nome: document.getElementById('nomeCriarTurma').value,
                    ativo: document.getElementById('ativoCriarTurma').checked
                };

                fetch('/Turma/AddTurma', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(turma)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            showToast(result.message, false);
                            $('#criarTurmaModal').modal('hide');
                            fetch('/Turma/GetAll')
                                .then(response => response.json())
                                .then(data => {
                                    turmaGridApi.setRowData(data);
                                });
                        } else {
                            showToast(result.message, true);
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao adicionar turma:', error);
                        showToast('Erro ao adicionar turma.', true);
                    });
            });
        }

        if (saveEditarTurmaBtn) {
            saveEditarTurmaBtn.addEventListener('click', function () {
                const turma = {
                    id: turmaToEdit,
                    nome: document.getElementById('nomeEditarTurma').value,
                    ativo: document.getElementById('ativoEditarTurma').checked
                };

                fetch('/Turma/EditTurma', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(turma)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            showToast(result.message, false);
                            $('#editarTurmaModal').modal('hide');
                            fetch('/Turma/GetAll')
                                .then(response => response.json())
                                .then(data => {
                                    turmaGridApi.setRowData(data);
                                });
                        } else {
                            showToast(result.message, true);
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao editar turma:', error);
                        showToast('Erro ao editar turma.', true);
                    });
            });
        }

        // Limpar modais ao fechar
        $('#criarTurmaModal').on('hidden.bs.modal', clearAddTurmaModalFields);
        $('#editarTurmaModal').on('hidden.bs.modal', clearEditTurmaModalFields);
    }

    if (document.querySelector('#alunoTurmaGrid')) {
        const saveAddAlunoTurmaBtn = document.getElementById('saveAddAlunoTurmaBtn');
        const saveEditAlunoTurmaBtn = document.getElementById('saveEditAlunoTurmaBtn');
        let selectedAlunos = [];
        let turmaIdEdit = null;

        const loadAlunos = () => {
            fetch('/AlunoTurma/GetAllAlunos')
                .then(response => response.json())
                .then(alunos => {
                    console.log('Alunos carregados:', alunos);
                    const alunoSelect = document.getElementById('alunoSelect');
                    const editAlunoAutocomplete = document.getElementById('editAlunoAutocomplete');

                    if (alunoSelect) {
                        alunoSelect.innerHTML = '';
                        alunos.forEach(aluno => {
                            const option = document.createElement('option');
                            option.value = aluno.id;
                            option.textContent = aluno.nome;
                            alunoSelect.appendChild(option);
                        });
                    }

                    if (editAlunoAutocomplete) {
                        $(editAlunoAutocomplete).autocomplete({
                            source: alunos.map(aluno => aluno.nome),
                            select: function (event, ui) {
                                const selectedAluno = alunos.find(aluno => aluno.nome === ui.item.value);
                                if (selectedAlunos.some(aluno => aluno.id === selectedAluno.id)) {
                                    showErrorMessage("Aluno já está na turma.");
                                } else {
                                    selectedAlunos.push(selectedAluno);
                                    renderAlunosRelacionados(selectedAlunos);
                                    vincularAlunoTurma(selectedAluno.id, turmaIdEdit)
                                        .then(() => {
                                            clearErrorMessage();
                                            showToast("Aluno vinculado com sucesso.", false);
                                            loadTurmas();
                                        })
                                        .catch(error => {
                                            showErrorMessage(error.message);
                                            showToast("Erro ao vincular aluno.", true);
                                        });
                                }
                                this.value = '';
                                return false;
                            }
                        });
                    }
                })
                .catch(error => {
                    console.error('Erro ao carregar alunos:', error);
                });
        };

        const loadTurmas = () => {
            fetch('/AlunoTurma/GetAllTurmas')
                .then(response => response.json())
                .then(turmas => {
                    console.log('Turmas carregadas:', turmas);
                    const turmaSelect = document.getElementById('turmaSelect');
                    const editTurmaSelect = document.getElementById('editTurmaSelect');

                    if (turmaSelect) {
                        turmaSelect.innerHTML = '';
                        turmas.forEach(turma => {
                            const option = document.createElement('option');
                            option.value = turma.id;
                            option.textContent = turma.nome;
                            turmaSelect.appendChild(option);
                        });
                    }

                    if (editTurmaSelect) {
                        editTurmaSelect.innerHTML = '';
                        turmas.forEach(turma => {
                            const option = document.createElement('option');
                            option.value = turma.id;
                            option.textContent = turma.nome;
                            editTurmaSelect.appendChild(option);
                        });
                    }

                    alunoTurmaGridApi.setRowData(turmas); // Atualiza a tabela de turmas
                })
                .catch(error => {
                    console.error('Erro ao carregar turmas:', error);
                });
        };

        const clearAddAlunoTurmaModalFields = () => {
            document.getElementById('alunoSelect').value = '';
            document.getElementById('turmaSelect').value = '';
        };

        const clearEditAlunoTurmaModalFields = () => {
            document.getElementById('editAlunoAutocomplete').value = '';
            document.getElementById('editTurmaSelect').value = '';
            document.getElementById('alunosRelacionados').innerHTML = '';
            selectedAlunos = [];
        };

        const renderAlunosRelacionados = (alunos) => {
            const alunosRelacionados = document.getElementById('alunosRelacionados');
            alunosRelacionados.innerHTML = '';
            alunos.forEach(aluno => {
                const li = document.createElement('li');
                li.classList.add('list-group-item');
                li.innerHTML = `
            ${aluno.nome}
            <button type="button" class="btn btn-sm btn-danger float-end" onclick="desvincularAluno(${aluno.id}, ${turmaIdEdit})">Desvincular</button>
        `;
                alunosRelacionados.appendChild(li);
            });
        };

        const showErrorMessage = (message) => {
            const errorMessageElement = document.getElementById('errorMessage');
            if (errorMessageElement) {
                errorMessageElement.textContent = message;
                errorMessageElement.style.display = 'block';
            }
        };

        const clearErrorMessage = () => {
            const errorMessageElement = document.getElementById('errorMessage');
            if (errorMessageElement) {
                errorMessageElement.textContent = '';
                errorMessageElement.style.display = 'none';
            }
        };

        window.desvincularAluno = (alunoId, turmaId) => {
            selectedAlunos = selectedAlunos.filter(aluno => aluno.id !== alunoId);
            renderAlunosRelacionados(selectedAlunos);
            fetch(`/AlunoTurma/DesvincularAlunoTurma?alunoId=${alunoId}&turmaId=${turmaId}`, { method: 'POST' })
                .then(response => response.json())
                .then(result => {
                    if (result.success) {
                        showToast(result.message, false);
                        loadTurmas();
                    } else {
                        showToast(result.message, true);
                    }
                })
                .catch(error => {
                    console.error('Erro ao desvincular aluno:', error);
                    showToast('Erro ao desvincular aluno.', true);
                });
        };

        window.inativarTurma = (id) => {
            console.log('Inativando turma com ID:', id); // Log para verificar o ID
            fetch(`/AlunoTurma/InativarAlunoTurma?id=${id}`, { method: 'POST' })
                .then(response => {
                    console.log('Resposta da inativação:', response); // Log para verificar a resposta
                    return response.json();
                })
                .then(result => {
                    console.log('Resultado da inativação:', result); // Log para verificar o resultado
                    if (result.success) {
                        showToast(result.message, false);
                        loadTurmas();
                    } else {
                        showToast(result.message, true);
                    }
                })
                .catch(error => {
                    console.error('Erro ao inativar turma:', error);
                    showToast('Erro ao inativar turma.', true);
                });
        };

        window.ativarTurma = (id) => {
            console.log('Ativando turma com ID:', id); // Log para verificar o ID
            fetch(`/AlunoTurma/AtivarAlunoTurma?id=${id}`, { method: 'POST' })
                .then(response => {
                    console.log('Resposta da ativação:', response); // Log para verificar a resposta
                    return response.json();
                })
                .then(result => {
                    console.log('Resultado da ativação:', result); // Log para verificar o resultado
                    if (result.success) {
                        showToast(result.message, false);
                        loadTurmas();
                    } else {
                        showToast(result.message, true);
                    }
                })
                .catch(error => {
                    console.error('Erro ao ativar turma:', error);
                    showToast('Erro ao ativar turma:', true);
                });
        };

        const vincularAlunoTurma = (alunoId, turmaId) => {
            const alunoTurma = {
                alunoId: alunoId,
                turmaId: turmaId
            };

            return fetch('/AlunoTurma/VincularAlunoTurma', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(alunoTurma)
            })
                .then(response => response.json())
                .then(result => {
                    if (!result.success) {
                        throw new Error(result.message);
                    }
                });
        };

        // Configuração do AG Grid para Relacionar Turmas
        const alunoTurmaColumnDefs = [
            { headerName: "Turma", field: "nome", sortable: true, filter: true, minWidth: 150 },
            {
                headerName: "Ativo", field: "ativo", sortable: true, filter: true, minWidth: 100, cellRenderer: function (params) {
                    return `<input type="checkbox" disabled ${params.value ? 'checked' : ''}>`;
                }
            },
            { headerName: "Quantidade de Alunos", field: "numeroAlunos", sortable: true, filter: true, minWidth: 150 },
            {
                headerName: "Ações", field: "acoes", minWidth: 150, cellRenderer: function (params) {
                    return `
                <button class="btn btn-sm btn-warning edit-btn" data-id="${params.data.id}">Editar</button>
                <button class="btn btn-sm btn-danger inactivate-btn" data-id="${params.data.id}" ${params.data.ativo ? '' : 'style="display:none;"'}>Inativar</button>
                <button class="btn btn-sm btn-success activate-btn" data-id="${params.data.id}" ${params.data.ativo ? 'style="display:none;"' : ''}>Ativar</button>
            `;
                }
            }
        ];

        const alunoTurmaGridOptions = {
            columnDefs: alunoTurmaColumnDefs,
            rowData: null,
            animateRows: true,
            pagination: true,
            paginationPageSize: 10,
            domLayout: 'autoHeight',
            localeText: localeText
        };

        const alunoTurmaGridDiv = document.querySelector('#alunoTurmaGrid');
        const alunoTurmaGridApi = agGrid.createGrid(alunoTurmaGridDiv, alunoTurmaGridOptions);

        fetch('/AlunoTurma/GetAllTurmas')
            .then(response => response.json())
            .then(data => {
                alunoTurmaGridApi.setRowData(data);
            })
            .catch(error => {
                console.error("Erro ao carregar dados:", error);
            });

        if (quickFilterInput) {
            quickFilterInput.addEventListener('input', function () {
                alunoTurmaGridApi.setQuickFilter(quickFilterInput.value);
            });
        }

        // Eventos de clique para editar, ativar e inativar associação
        alunoTurmaGridDiv.addEventListener('click', function (e) {
            const target = e.target;
            const id = target.getAttribute('data-id');
            console.log("Button clicked, ID:", id);

            if (target.classList.contains('edit-btn')) {
                fetch(`/AlunoTurma/GetAlunosByTurma?turmaId=${id}`)
                    .then(response => response.json())
                    .then(alunos => {
                        document.getElementById('editTurmaSelect').value = id;
                        selectedAlunos = alunos;
                        renderAlunosRelacionados(alunos);

                        $('#editAlunoTurmaModal').modal('show');
                        turmaIdEdit = id;
                    })
                    .catch(error => {
                        console.error('Erro ao buscar detalhes da associação:', error);
                    });
            } else if (target.classList.contains('inactivate-btn')) {
                showModal('Tem certeza de que deseja inativar esta associação?', () => {
                    console.log('Inativando associação com ID:', id);
                    inativarTurma(id);
                    target.style.display = 'none';
                    target.nextElementSibling.style.display = 'inline-block';
                });
            } else if (target.classList.contains('activate-btn')) {
                showModal('Tem certeza de que deseja ativar esta associação?', () => {
                    console.log('Ativando associação com ID:', id);
                    ativarTurma(id);
                    target.style.display = 'none';
                    target.previousElementSibling.style.display = 'inline-block';
                });
            }
        });

        if (saveAddAlunoTurmaBtn) {
            saveAddAlunoTurmaBtn.addEventListener('click', function () {
                const alunoTurma = {
                    alunoId: document.getElementById('alunoSelect').value,
                    turmaId: document.getElementById('turmaSelect').value
                };

                fetch('/AlunoTurma/AddAlunoTurma', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(alunoTurma)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            showToast(result.message, false);
                            $('#addAlunoTurmaModal').modal('hide');
                            loadTurmas();
                        } else {
                            showToast(result.message, true);
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao adicionar associação:', error);
                        showToast('Erro ao adicionar associação.', true);
                    });
            });
        }

        if (saveEditAlunoTurmaBtn) {
            saveEditAlunoTurmaBtn.addEventListener('click', function () {
                const alunoTurma = {
                    alunoId: document.getElementById('editAlunoAutocomplete').value,
                    turmaId: document.getElementById('editTurmaSelect').value
                };

                fetch('/AlunoTurma/EditAlunoTurma', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(alunoTurma)
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            showToast(result.message, false);
                            $('#editAlunoTurmaModal').modal('hide');
                            loadTurmas();
                        } else {
                            showToast(result.message, true);
                        }
                    })
                    .catch(error => {
                        console.error('Erro ao editar associação:', error);
                        showToast('Erro ao editar associação.', true);
                    });
            });
        }

        // Limpar modais ao fechar
        $('#addAlunoTurmaModal').on('hidden.bs.modal', clearAddAlunoTurmaModalFields);
        $('#editAlunoTurmaModal').on('hidden.bs.modal', clearEditAlunoTurmaModalFields);

        // Carregar alunos e turmas ao abrir o modal
        $('#addAlunoTurmaModal').on('shown.bs.modal', () => {
            loadAlunos();
            loadTurmas();
        });

        $('#editAlunoTurmaModal').on('shown.bs.modal', () => {
            loadAlunos();
            loadTurmas();
        });

        // Chamar a função de auto completar ao focar no input de alunos
        document.getElementById('editAlunoAutocomplete').addEventListener('focus', function () {
            console.log('Auto completar focado');
            loadAlunos();
        });
    }




});
